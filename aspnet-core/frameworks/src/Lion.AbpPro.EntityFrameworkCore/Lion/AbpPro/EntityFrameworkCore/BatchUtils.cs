namespace Lion.AbpPro.EntityFrameworkCore
{
    public static class BatchUtils
    {
		public static bool IsNullableType(Type type)
        {
			return type.IsGenericType
					&& type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		private static IProperty GetPKProperty<TEntity>(DbSet<TEntity> dbSet) where TEntity : class
		{
            var pkProps = dbSet.EntityType.FindPrimaryKey().Properties;
            if (pkProps.Count != 1)
            {
                throw new ArgumentException("Only entity types with one single primary key are supported.");
            }
			return pkProps[0];

        }


        public static string GetPKColName<TEntity>(DbSet<TEntity> dbSet) where TEntity : class
		{
			var pkProp = GetPKProperty<TEntity>(dbSet);
			string pkColName = pkProp.GetColumnName(StoreObjectIdentifier.SqlQuery(dbSet.EntityType));
			return pkColName;
        }

		private static Expression<Func<TEntity,object>> GetSelectPkExpression<TEntity>(DbContext dbCtx) where TEntity : class
        {

            var parameter = Expression.Parameter(typeof(TEntity), "e");
			var pkProp = GetPKProperty<TEntity>(dbCtx.Set<TEntity>());
			string pkPropName = pkProp.Name;
            
            var body = Expression.Convert(Expression.MakeMemberAccess(parameter, typeof(TEntity).GetProperty(pkPropName)),typeof(object));
			return (Expression<Func<TEntity, object>>)Expression.Lambda(body, parameter);
        }

		public static string BuildWhereSubQuery<TEntity>(IQueryable<TEntity> queryable, DbContext dbCtx, string aliasSeparator) where TEntity : class
		{
            IQueryProvider queryProvider = queryable.Provider;
            IQueryable whereQuerable = queryable.Select(GetSelectPkExpression<TEntity>(dbCtx));

			/*IRelationalQueryingEnumerable? queryingEnumerable = queryable.Provider.Execute<IEnumerable>(queryable.Expression) as IRelationalQueryingEnumerable;*/
            IRelationalQueryingEnumerable? queryingEnumerable = queryProvider.Execute<IEnumerable>(whereQuerable.Expression) as IRelationalQueryingEnumerable;
            if (queryingEnumerable==null)
            {
				throw new ApplicationException("Can't get IRelationalQueryingEnumerable from Expression");
			}
			string subQuerySQL;
			using (var cmd = queryingEnumerable.CreateDbCommand())
			{
				subQuerySQL = cmd.CommandText;
			}
            //string tableAlias = BatchUtils.UniqueAlias();
            string tableAlias = "temp1";
            var dbSet = dbCtx.Set<TEntity>();
			string pkName = BatchUtils.GetPKColName<TEntity>(dbSet);
			ISqlGenerationHelper sqlGenHelpr = dbCtx.GetService<ISqlGenerationHelper>();
			string quotedPkName = sqlGenHelpr.DelimitIdentifier(pkName);//pkId-->"pdId" on NPgsql
			StringBuilder sbSQL = new StringBuilder();
            //sbSQL.Append(quotedPkName).Append(" IN(").Append(subQuerySQL).AppendLine(")");
            //if not put a duplicate subquery, an error will be throw no Mysql: You can't specify target table 'T_Comments' for update in FROM clause
			if(dbCtx.Database.ProviderName.Contains("mysql",StringComparison.OrdinalIgnoreCase))
			{
                sbSQL.Append(quotedPkName).Append(" IN(SELECT ").Append(quotedPkName).Append(" FROM (")
					.Append(subQuerySQL).AppendLine($") {aliasSeparator} {tableAlias} )");
            }
			else
			{
                sbSQL.Append(quotedPkName).Append(" IN(").Append(subQuerySQL).AppendLine(")");
            }
            return sbSQL.ToString();
		}

		/// <summary>
		/// exclude the oldSQL from newSQL
		/// Diff("abc","abc12")=="12"
		/// </summary>
		/// <param name="oldSQL"></param>
		/// <param name="newSQL"></param>
		/// <returns></returns>
		public static string Diff(string oldSQL, string newSQL)
		{
			if (!newSQL.StartsWith(oldSQL))
			{
				throw new ArgumentException("!newSQL.StartsWith(oldSQL)", nameof(newSQL));
			}
			return newSQL.Substring(oldSQL.Length);
		}

		//this method is from source code ef core
		public static void GenerateList<T>(IReadOnlyList<T> items, IRelationalCommandBuilder Sql, Action<T> generationAction, Action<IRelationalCommandBuilder> joinAction = null)
		{
			if (joinAction == null)
			{
				joinAction = delegate (IRelationalCommandBuilder isb)
				{
					isb.Append(", ");
				};
			}
			for (int i = 0; i < items.Count; i++)
			{
				if (i > 0)
				{
					joinAction(Sql);
				}
				generationAction(items[i]);
			}
		}

		//this method is from source code ef core
		public static bool IsNonComposedSetOperation(SelectExpression selectExpression)
		{
			if (selectExpression.Offset == null && selectExpression.Limit == null && !selectExpression.IsDistinct && selectExpression.Predicate == null && selectExpression.Having == null && selectExpression.Orderings.Count == 0 && selectExpression.GroupBy.Count == 0 && selectExpression.Tables.Count == 1)
			{
				TableExpressionBase tableExpressionBase = selectExpression.Tables[0];
				SetOperationBase setOperation = tableExpressionBase as SetOperationBase;
				if (setOperation != null && selectExpression.Projection.Count == setOperation.Source1.Projection.Count)
				{
					return selectExpression.Projection.Select(delegate (ProjectionExpression pe, int index)
					{
						ColumnExpression columnExpression = pe.Expression as ColumnExpression;
						if (columnExpression != null && string.Equals(columnExpression.Table.Alias, setOperation.Alias, StringComparison.OrdinalIgnoreCase))
						{
							return string.Equals(columnExpression.Name, setOperation.Source1.Projection[index].Alias, StringComparison.OrdinalIgnoreCase);
						}
						return false;
					}).All((bool e) => e);
				}
			}
			return false;
		}

		public static void OpenIfNeeded(this IDbConnection conn )
        {
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}
		}

		public static Task OpenIfNeededAsync(this DbConnection conn,CancellationToken cancellationToken=default)
		{
			if (conn.State != ConnectionState.Open)
			{
				return conn.OpenAsync(cancellationToken);
			}
            else
            {
				return Task.CompletedTask;
            }
		}

		public static IDictionary<string,object> ConvertParameterValues(this DbContext ctx, IReadOnlyDictionary<string, object> modelValues)
        {
			var typeMapping = ctx.GetService<IRelationalTypeMappingSource>();
			Dictionary<string, object> providerValues = new ();
			foreach (var kvp in modelValues)
			{
				string key = kvp.Key;
				object value = modelValues[key];
				providerValues[key] = ConvertToProvider(typeMapping,value);
			}
			return providerValues;
		}

		private static object ConvertToProvider(IRelationalTypeMappingSource typeMappingSource,object modelObject)
        {
			if(modelObject==null)
            {
				return DBNull.Value;
            }
			var mp = typeMappingSource.FindMapping(modelObject.GetType());
			if (mp == null || mp.Converter == null || mp.Converter.ConvertToProvider == null)
			{
				return modelObject;

			}
			object providerValue = mp.Converter.ConvertToProvider(modelObject);
			if (providerValue == null)
			{
				return DBNull.Value;
			}
			return providerValue;
		}
	}
}
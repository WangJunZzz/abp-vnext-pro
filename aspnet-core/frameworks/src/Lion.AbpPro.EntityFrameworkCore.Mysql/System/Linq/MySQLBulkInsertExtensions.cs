namespace System.Linq
{
    public static class MySQLBulkInsertExtensions
    {
        public static async Task BulkInsertAsync<TEntity>(this DbContext dbCtx, IEnumerable<TEntity> items, MySqlTransaction transaction = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            var conn = dbCtx.Database.GetDbConnection();
            await conn.OpenIfNeededAsync(cancellationToken);
            var dataTable = BulkInsertUtils.BuildDataTable(dbCtx, dbCtx.Set<TEntity>(), items);
            var bulkCopy = BuildSqlBulkCopy<TEntity>((MySqlConnection)conn, dbCtx, transaction);
            await bulkCopy.WriteToServerAsync(dataTable, cancellationToken);
        }

        public static void BulkInsert<TEntity>(this DbContext dbCtx, IEnumerable<TEntity> items, MySqlTransaction transaction = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            var conn = dbCtx.Database.GetDbConnection();
            conn.OpenIfNeeded();
            var dataTable = BulkInsertUtils.BuildDataTable(dbCtx, dbCtx.Set<TEntity>(), items);
            var bulkCopy = BuildSqlBulkCopy<TEntity>((MySqlConnection)conn, dbCtx, transaction);
            bulkCopy.WriteToServer(dataTable);
        }

        private static MySqlBulkCopy BuildSqlBulkCopy<TEntity>(MySqlConnection conn, DbContext dbCtx, MySqlTransaction transaction = null) where TEntity : class
        {
            var dbSet = dbCtx.Set<TEntity>();
            var entityType = dbSet.EntityType;
            var dbProps = BulkInsertUtils.ParseDbProps<TEntity>(dbCtx, entityType);

            var bulkCopy = new MySqlBulkCopy(conn, transaction)
            {
                DestinationTableName = entityType.GetTableName() //Schema is not supported by MySQL
            };

            var sourceOrdinal = 0;
            foreach (var dbProp in dbProps)
            {
                var columnName = dbProp.ColumnName;
                bulkCopy.ColumnMappings.Add(new MySqlBulkCopyColumnMapping(sourceOrdinal, columnName));
                sourceOrdinal++;
            }

            return bulkCopy;
        }
    }
}

using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using MySql.Data.MySqlClient;

public static class EfDapperBulkExtensions
{
    /// <summary>
    /// 使用 EF Core metadata 确保表名、字段名和顺序一致性进行批量插入
    /// </summary>
    public static async Task<int> BulkInsertAsync<T>(
        this IDbConnection connection,
        DbContext dbContext,
        IEnumerable<T> entities,
        IDbTransaction transaction = null,
        CancellationToken cancellationToken = default) where T : class
    {
        // 获取 EF Core 实体类型信息
        var entityType = dbContext.Model.FindEntityType(typeof(T));
        if (entityType == null)
            throw new InvalidOperationException($"Entity type {typeof(T).Name} not found in DbContext model.");

        // 获取表名
        var tableName = entityType.GetTableName();
        var schemaName = entityType.GetSchema();
        if (string.IsNullOrEmpty(tableName))
            throw new InvalidOperationException($"Table name not found for entity {typeof(T).Name}.");

        // 获取属性映射信息，确保顺序一致性
        var properties = GetPropertiesInOrder<T>(entityType);

        // 创建 DataTable
        var dataTable = CreateDataTable<T>(entities.ToList(), properties, tableName, schemaName);

        // 执行批量插入
        return await BulkInsertDataTableAsync(connection, dataTable, cancellationToken);
    }

    /// <summary>
    /// 创建 DataTable 用于批量插入
    /// </summary>
    private static DataTable CreateDataTable<T>(List<T> entities, List<PropertyMappingInfo> properties, string tableName, string schemaName) where T : class
    {
        var dataTable = new DataTable();
        dataTable.TableName = string.IsNullOrEmpty(schemaName) ? tableName : $"{schemaName}.{tableName}";

        // 添加列，按照 EF Core 中定义的顺序
        foreach (var property in properties)
        {
            var columnType = GetDbType(property.Property.ClrType);
            var column = new DataColumn(property.ColumnName, columnType);
            dataTable.Columns.Add(column);
        }

        // 添加行
        foreach (var entity in entities)
        {
            var row = dataTable.NewRow();
            foreach (var property in properties)
            {
                var value = property.PropertyInfo.GetValue(entity) ?? DBNull.Value;
                row[property.ColumnName] = value;
            }
            dataTable.Rows.Add(row);
        }

        return dataTable;
    }

    /// <summary>
    /// 将 DataTable 批量插入到 MySQL
    /// </summary>
    private static async Task<int> BulkInsertDataTableAsync(IDbConnection connection, DataTable dataTable, CancellationToken cancellationToken)
    {
        var mySqlConnection = connection as MySqlConnection;
        if (mySqlConnection == null)
            throw new InvalidOperationException("Connection must be MySqlConnection for bulk insert");

        if (dataTable.Rows.Count == 0)
            return 0;

        // 确保连接打开
        if (mySqlConnection.State != ConnectionState.Open)
        {
            await mySqlConnection.OpenAsync(cancellationToken);
        }

        // 使用 MySqlBulkLoader 进行批量插入
        var bulkLoader = new MySqlBulkLoader(mySqlConnection)
        {
            TableName = dataTable.TableName,
            FieldTerminator = "\t",
            LineTerminator = "\n",
            NumberOfLinesToSkip = 0
        };

        // 添加字段映射，确保与 DataTable 列顺序一致
        foreach (DataColumn column in dataTable.Columns)
        {
            bulkLoader.Columns.Add(column.ColumnName);
        }

        // 写入数据到临时文件
        var tempFilePath = Path.GetTempFileName();
        try
        {
            using (var writer = new StreamWriter(tempFilePath, false, System.Text.Encoding.UTF8))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var values = new object[dataTable.Columns.Count];
                    row.ItemArray.CopyTo(values, 0);
                    var line = string.Join("\t", values.Select(v => v?.ToString()?.Replace("\t", "\\t").Replace("\n", "\\n") ?? "\\N"));
                    await writer.WriteLineAsync(line);
                }
            }

            bulkLoader.FileName = tempFilePath;
            var result = await bulkLoader.LoadAsync();
            
            return result;
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }
    }

    /// <summary>
    /// 获取属性信息，确保与 EF Core 中定义的顺序一致
    /// </summary>
    private static List<PropertyMappingInfo> GetPropertiesInOrder<T>(IEntityType entityType) where T : class
    {
        var properties = new List<PropertyMappingInfo>();
        
        // 按 EF Core 中属性的顺序获取
        foreach (var property in entityType.GetProperties())
        {
            // 跳过 Shadow Properties（影子属性）
            if (property.IsShadowProperty())
                continue;

            var propertyInfo = typeof(T).GetProperty(property.Name, BindingFlags.Instance | BindingFlags.Public);
            if (propertyInfo != null && propertyInfo.CanRead && propertyInfo.CanWrite)
            {
                properties.Add(new PropertyMappingInfo
                {
                    PropertyInfo = propertyInfo,
                    ColumnName = property.GetColumnName(),
                    Property = property
                });
            }
        }

        return properties;
    }

    /// <summary>
    /// 将 .NET 类型转换为 DbType
    /// </summary>
    private static Type GetDbType(Type clrType)
    {
        // 处理可空类型
        if (clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            clrType = Nullable.GetUnderlyingType(clrType);
        }

        return clrType switch
        {
            Type t when t == typeof(string) => typeof(string),
            Type t when t == typeof(int) => typeof(int),
            Type t when t == typeof(long) => typeof(long),
            Type t when t == typeof(decimal) => typeof(decimal),
            Type t when t == typeof(double) => typeof(double),
            Type t when t == typeof(float) => typeof(float),
            Type t when t == typeof(bool) => typeof(bool),
            Type t when t == typeof(DateTime) => typeof(DateTime),
            Type t when t == typeof(Guid) => typeof(Guid),
            Type t when t == typeof(byte[]) => typeof(byte[]),
            _ => typeof(object)
        };
    }

    private class PropertyMappingInfo
    {
        public PropertyInfo PropertyInfo { get; set; }
        public string ColumnName { get; set; }
        public IProperty Property { get; set; }
    }
}
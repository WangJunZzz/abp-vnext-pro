namespace Lion.AbpPro.CAP.EntityFrameworkCore;

public class LionAbpProLionAbpProCapDbProviderInfoProvider : ILionAbpProCapDbProviderInfoProvider, ITransientDependency
{
    protected ConcurrentDictionary<string, LionAbpProCapDbProviderInfo> CapDbProviderInfos { get; set; } = new();

    public virtual LionAbpProCapDbProviderInfo GetOrNull(string dbProviderName)
    {
        return CapDbProviderInfos.GetOrAdd(dbProviderName, InternalGetOrNull);
    }
    
    protected virtual LionAbpProCapDbProviderInfo InternalGetOrNull(string databaseProviderName)
    {
        switch (databaseProviderName)
        {
            case "Microsoft.EntityFrameworkCore.SqlServer":
                return new LionAbpProCapDbProviderInfo(
                    "DotNetCore.CAP.SqlServerCapTransaction, DotNetCore.CAP.SqlServer",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.SqlServer");
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                return new LionAbpProCapDbProviderInfo(
                    "DotNetCore.CAP.PostgreSqlCapTransaction, DotNetCore.CAP.PostgreSql",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.PostgreSQL");
            case "Pomelo.EntityFrameworkCore.MySql":
                return new LionAbpProCapDbProviderInfo(
                    "DotNetCore.CAP.MySqlCapTransaction, DotNetCore.CAP.MySql",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.MySql");
            case "Oracle.EntityFrameworkCore":
            case "Devart.Data.Oracle.Entity.EFCore":
                return new LionAbpProCapDbProviderInfo(
                    "DotNetCore.CAP.OracleCapTransaction, DotNetCore.CAP.Oracle",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.Oracle");
            case "Microsoft.EntityFrameworkCore.Sqlite":
                return new LionAbpProCapDbProviderInfo(
                    "DotNetCore.CAP.SqliteCapTransaction, DotNetCore.CAP.Sqlite",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.Sqlite");
            case "Microsoft.EntityFrameworkCore.InMemory":
                return new LionAbpProCapDbProviderInfo(
                    "DotNetCore.CAP.InMemoryCapTransaction, DotNetCore.CAP.InMemoryStorage",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.InMemoryStorage");
            default:
                return null;
        }
    }
}
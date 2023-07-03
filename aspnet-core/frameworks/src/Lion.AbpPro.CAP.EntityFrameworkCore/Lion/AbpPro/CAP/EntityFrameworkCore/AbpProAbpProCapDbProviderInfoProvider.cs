namespace Lion.AbpPro.CAP.EntityFrameworkCore;

public class AbpProAbpProCapDbProviderInfoProvider : IAbpProCapDbProviderInfoProvider, ITransientDependency
{
    protected ConcurrentDictionary<string, AbpProCapDbProviderInfo> CapDbProviderInfos { get; set; } = new();

    public virtual AbpProCapDbProviderInfo GetOrNull(string dbProviderName)
    {
        return CapDbProviderInfos.GetOrAdd(dbProviderName, InternalGetOrNull);
    }
    
    protected virtual AbpProCapDbProviderInfo InternalGetOrNull(string databaseProviderName)
    {
        switch (databaseProviderName)
        {
            case "Microsoft.EntityFrameworkCore.SqlServer":
                return new AbpProCapDbProviderInfo(
                    "DotNetCore.CAP.SqlServerCapTransaction, DotNetCore.CAP.SqlServer",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.SqlServer");
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                return new AbpProCapDbProviderInfo(
                    "DotNetCore.CAP.PostgreSqlCapTransaction, DotNetCore.CAP.PostgreSql",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.PostgreSQL");
            case "Pomelo.EntityFrameworkCore.MySql":
                return new AbpProCapDbProviderInfo(
                    "DotNetCore.CAP.MySqlCapTransaction, DotNetCore.CAP.MySql",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.MySql");
            case "Oracle.EntityFrameworkCore":
            case "Devart.Data.Oracle.Entity.EFCore":
                return new AbpProCapDbProviderInfo(
                    "DotNetCore.CAP.OracleCapTransaction, DotNetCore.CAP.Oracle",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.Oracle");
            case "Microsoft.EntityFrameworkCore.Sqlite":
                return new AbpProCapDbProviderInfo(
                    "DotNetCore.CAP.SqliteCapTransaction, DotNetCore.CAP.Sqlite",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.Sqlite");
            case "Microsoft.EntityFrameworkCore.InMemory":
                return new AbpProCapDbProviderInfo(
                    "DotNetCore.CAP.InMemoryCapTransaction, DotNetCore.CAP.InMemoryStorage",
                    "Microsoft.EntityFrameworkCore.Storage.CapEFDbTransaction, DotNetCore.CAP.InMemoryStorage");
            default:
                return null;
        }
    }
}
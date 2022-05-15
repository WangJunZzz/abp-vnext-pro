using System;
using System.Linq;
using Lion.AbpPro.EntityFrameworkCore;
using Lion.AbpPro.FreeSqlRepository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.FreeSqlReppsitory.Tests;

[DependsOn(
    typeof(AbpProTestBaseModule),
    typeof(AbpProEntityFrameworkCoreTestModule),
    typeof(AbpProFreeSqlModule)
)]
public class AbpProFreeSqlRepositoryTestModule : AbpModule
{
    private const string ConnectionString = "Data Source=:memory:";
    private SqliteConnection _sqliteConnection;

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
       
        var freeSql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, ConnectionString)
            .UseAutoSyncStructure(true)
            .Build();
        context.Services.AddSingleton<IFreeSql>(freeSql);
        var connection = new SqliteConnection(ConnectionString);
        ConfigureInMemorySqlite(context.Services, freeSql);
    }


    private void ConfigureInMemorySqlite(IServiceCollection services, IFreeSql freeSql)
    {
        _sqliteConnection = CreateDatabaseAndGetConnection(freeSql);

        //services.Configure<AbpDbContextOptions>(options =>
        //{
        //    options.PreConfigure<AbpProDbContext>(options => { options.DbContextOptions.UseBatchEF_Sqlite(); });
        //    options.Configure(context => { context.DbContextOptions.UseSqlite(_sqliteConnection); });
        //});
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        // _sqliteConnection.Dispose();
    }

    private static SqliteConnection CreateDatabaseAndGetConnection(IFreeSql freeSql)
    {
        var connection = new SqliteConnection(ConnectionString);
         //connection.Open();

        var options = new DbContextOptionsBuilder<AbpProDbContext>()
             .UseSqlite(connection)
             .Options;

        using (var context = new AbpProDbContext(options))
        {
            foreach (var entityType in context.Model.GetEntityTypes())
            {
                freeSql.CodeFirst.SyncStructure(entityType.ClrType, entityType.GetTableName(), true);
            }

            //context.GetService<IRelationalDatabaseCreator>().CreateTables();
        }
        
        return connection;
    }
}
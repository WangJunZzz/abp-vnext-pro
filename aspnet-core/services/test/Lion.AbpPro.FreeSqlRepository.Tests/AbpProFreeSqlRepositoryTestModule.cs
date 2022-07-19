namespace Lion.AbpPro.FreeSqlReppsitory.Tests;

[DependsOn(
    typeof(AbpProTestBaseModule),
    typeof(AbpProEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqliteModule),
    typeof(AbpProFreeSqlModule)
)]
public class AbpProFreeSqlRepositoryTestModule : AbpModule
{
    private SqliteConnection _sqliteConnection;
    private const string ConnectionString = "Data Source=:memory:";
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var freeSql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, ConnectionString)
            .UseAutoSyncStructure(true)
            .Build();
        context.Services.AddSingleton<IFreeSql>(freeSql);
        ConfigureInMemorySqlite(context.Services,freeSql);
  
    }

    private void ConfigureInMemorySqlite(IServiceCollection services,IFreeSql freeSql)
    {
        _sqliteConnection = CreateDatabaseAndGetConnection(freeSql);

        services.Configure<AbpDbContextOptions>(options =>
        {
            options.PreConfigure<AbpProDbContext>(options =>
            {
                options.DbContextOptions.UseBatchEF_Sqlite();
            });
            options.Configure(context =>
            {
                context.DbContextOptions.UseSqlite(_sqliteConnection);
            });
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        _sqliteConnection.Dispose();
    }

    private static SqliteConnection CreateDatabaseAndGetConnection(IFreeSql freeSql)
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var options = new DbContextOptionsBuilder<AbpProDbContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new AbpProDbContext(options))
        {
            foreach (var entityType in context.Model.GetEntityTypes())
            {
                freeSql.CodeFirst.SyncStructure(entityType.ClrType, entityType.GetTableName(), true);
            }
            context.GetService<IRelationalDatabaseCreator>().CreateTables();
        }

        return connection;
    }
    
}
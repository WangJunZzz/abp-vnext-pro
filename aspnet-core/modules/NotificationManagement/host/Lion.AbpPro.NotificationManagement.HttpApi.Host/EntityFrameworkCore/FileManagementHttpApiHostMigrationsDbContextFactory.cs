namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore;

public class NotificationManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<NotificationManagementHttpApiHostMigrationsDbContext>
{
    public NotificationManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<NotificationManagementHttpApiHostMigrationsDbContext>()
            .UseMySql(configuration.GetConnectionString(NotificationManagementDbProperties.ConnectionStringName), MySqlServerVersion.LatestSupportedServerVersion);
        return new NotificationManagementHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}

namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore;

public class NotificationManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<NotificationManagementHttpApiHostMigrationsDbContext>
{
    public NotificationManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<NotificationManagementHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default") ?? string.Empty);
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

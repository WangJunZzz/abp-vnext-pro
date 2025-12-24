using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lion.AbpPro.BasicManagement.EntityFrameworkCore;

public class BasicManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<BasicManagementHttpApiHostMigrationsDbContext>
{
    public BasicManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        var builder = new DbContextOptionsBuilder<BasicManagementHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default") ?? string.Empty);

        return new BasicManagementHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);
        return builder.Build();
    }
}

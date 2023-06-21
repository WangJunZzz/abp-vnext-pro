using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.EntityFrameworkCore;

public class TestsHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<TestsHttpApiHostMigrationsDbContext>
{
    public TestsHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        var builder = new DbContextOptionsBuilder<TestsHttpApiHostMigrationsDbContext>()
            .UseMySql(configuration.GetConnectionString("Tests"), MySqlServerVersion.LatestSupportedServerVersion);
        return new TestsHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}

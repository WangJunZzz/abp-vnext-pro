using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lion.AbpPro.AuditLoggingManagement.HttpApi.Host.EntityFrameworkCore
{
    public class AuditLoggingManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<AuditLoggingManagementHttpApiHostMigrationsDbContext>
    {
        public AuditLoggingManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<AuditLoggingManagementHttpApiHostMigrationsDbContext>()
                .UseMySql(configuration.GetConnectionString("AbpAuditLogging"), MySqlServerVersion.LatestSupportedServerVersion);
            return new AuditLoggingManagementHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
            return builder.Build();
        }
    }
}

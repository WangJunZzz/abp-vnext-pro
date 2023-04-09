namespace Lion.AbpPro.LanguageManagement.EntityFrameworkCore
{
    public class LanguageManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<LanguageManagementHttpApiHostMigrationsDbContext>
    {
        public LanguageManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<LanguageManagementHttpApiHostMigrationsDbContext>()
                .UseMySql(configuration.GetConnectionString("LanguageManagement"), MySqlServerVersion.LatestSupportedServerVersion);
            return new LanguageManagementHttpApiHostMigrationsDbContext(builder.Options);
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

namespace Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore
{
    public class DataDictionaryManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DataDictionaryManagementHttpApiHostMigrationsDbContext>
    {
        public DataDictionaryManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<DataDictionaryManagementHttpApiHostMigrationsDbContext>()
                .UseMySql(configuration.GetConnectionString("DataDictionaryManagement"), MySqlServerVersion.LatestSupportedServerVersion);
            return new DataDictionaryManagementHttpApiHostMigrationsDbContext(builder.Options);
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

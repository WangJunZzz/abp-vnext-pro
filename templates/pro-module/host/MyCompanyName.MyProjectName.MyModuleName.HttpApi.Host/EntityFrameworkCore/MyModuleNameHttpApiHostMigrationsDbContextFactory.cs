namespace MyCompanyName.MyProjectName.MyModuleName.EntityFrameworkCore
{
    public class MyModuleNameHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<MyModuleNameHttpApiHostMigrationsDbContext>
    {
        public MyModuleNameHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MyModuleNameHttpApiHostMigrationsDbContext>()
                .UseMySQL(configuration.GetConnectionString("MyModuleName") ?? string.Empty);
            return new MyModuleNameHttpApiHostMigrationsDbContext(builder.Options);
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

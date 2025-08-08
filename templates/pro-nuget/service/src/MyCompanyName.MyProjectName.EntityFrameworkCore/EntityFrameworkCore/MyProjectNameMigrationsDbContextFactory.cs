namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class MyProjectNameMigrationsDbContextFactory : IDesignTimeDbContextFactory<MyProjectNameDbContext>
    {
        public MyProjectNameDbContext CreateDbContext(string[] args)
        {
            MyProjectNameEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MyProjectNameDbContext>()
                .UseMySQL(configuration.GetConnectionString("Default") ?? string.Empty);

            return new MyProjectNameDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath
                (
                    Path.Combine
                    (
                        Directory.GetCurrentDirectory(),
                        "../MyCompanyName.MyProjectName.DbMigrator/"
                    )
                )
                .AddJsonFile
                (
                    "appsettings.json",
                    false
                );

            return builder.Build();
        }
    }
}
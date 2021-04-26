using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace CompanyNameProjectName.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class CompanyNameProjectNameMigrationsDbContextFactory : IDesignTimeDbContextFactory<CompanyNameProjectNameMigrationsDbContext>
    {
        public CompanyNameProjectNameMigrationsDbContext CreateDbContext(string[] args)
        {
            CompanyNameProjectNameEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            //var builder = new DbContextOptionsBuilder<CompanyNameProjectNameMigrationsDbContext>()
            //    .UseSqlServer(configuration.GetConnectionString("Default"));
            var builder = new DbContextOptionsBuilder<CompanyNameProjectNameMigrationsDbContext>()
            .UseMySql(configuration.GetConnectionString("Default"), new MySqlServerVersion(new Version(5, 7)), mySqlOptions => mySqlOptions
                           .CharSetBehavior(CharSetBehavior.NeverAppend));

            return new CompanyNameProjectNameMigrationsDbContext(builder.Options);
        }

        //private static IConfigurationRoot BuildConfiguration()
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CompanyNameProjectName.DbMigrator/"))
        //        .AddJsonFile("appsettings.json", optional: false);

        //    return builder.Build();
        //}

        private static IConfigurationRoot BuildConfiguration()
        {
            var path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(path, "tools", "CompanyName.ProjectName.DbMigrator"))
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

    }
}

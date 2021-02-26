using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Zzz.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class ZzzMigrationsDbContextFactory : IDesignTimeDbContextFactory<ZzzMigrationsDbContext>
    {
        public ZzzMigrationsDbContext CreateDbContext(string[] args)
        {
            ZzzEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            //var builder = new DbContextOptionsBuilder<ZzzMigrationsDbContext>()
            //    .UseSqlServer(configuration.GetConnectionString("Default"));
            var builder = new DbContextOptionsBuilder<ZzzMigrationsDbContext>()
            .UseMySql(configuration.GetConnectionString("Default"), new MySqlServerVersion(new Version(5, 7)), mySqlOptions => mySqlOptions
                           .CharSetBehavior(CharSetBehavior.NeverAppend));

            return new ZzzMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Zzz.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CompanyNameProjectName.Data;
using Volo.Abp.DependencyInjection;

namespace CompanyNameProjectName.EntityFrameworkCore
{
    public class EntityFrameworkCoreCompanyNameProjectNameDbSchemaMigrator
        : ICompanyNameProjectNameDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreCompanyNameProjectNameDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the CompanyNameProjectNameMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<CompanyNameProjectNameMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
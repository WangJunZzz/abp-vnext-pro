using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CompanyName.ProjectName.Data;
using Volo.Abp.DependencyInjection;

namespace CompanyName.ProjectName.EntityFrameworkCore
{
    public class EntityFrameworkCoreProjectNameDbSchemaMigrator
        : IProjectNameDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreProjectNameDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ProjectNameMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ProjectNameMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
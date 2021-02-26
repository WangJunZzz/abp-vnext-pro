using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zzz.Data;
using Volo.Abp.DependencyInjection;

namespace Zzz.EntityFrameworkCore
{
    public class EntityFrameworkCoreZzzDbSchemaMigrator
        : IZzzDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreZzzDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ZzzMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ZzzMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
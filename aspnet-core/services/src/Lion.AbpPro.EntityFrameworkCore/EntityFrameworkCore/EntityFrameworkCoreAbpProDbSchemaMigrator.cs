namespace Lion.AbpPro.EntityFrameworkCore
{
    public class EntityFrameworkCoreAbpProDbSchemaMigrator
        : IAbpProDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreAbpProDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the AbpProMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<AbpProDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
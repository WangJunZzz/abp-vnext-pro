namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    public class EntityFrameworkCoreMyProjectNameDbSchemaMigrator
        : IMyProjectNameDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreMyProjectNameDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the MyProjectNameMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<MyProjectNameDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
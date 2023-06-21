using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Data;

public class TestsEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public TestsEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the TestsDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TestsDbContext>()
            .Database
            .MigrateAsync();
    }
}

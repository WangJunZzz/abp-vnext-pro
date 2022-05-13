using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Lion.AbpPro.Data;

/* This is used if database provider does't define
 * IAbpProDbSchemaMigrator implementation.
 */
public class NullAbpProDbSchemaMigrator : IAbpProDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

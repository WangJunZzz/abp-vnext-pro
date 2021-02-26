using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Zzz.Data
{
    /* This is used if database provider does't define
     * IZzzDbSchemaMigrator implementation.
     */
    public class NullZzzDbSchemaMigrator : IZzzDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
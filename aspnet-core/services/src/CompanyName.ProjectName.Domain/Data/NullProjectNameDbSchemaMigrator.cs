using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CompanyName.ProjectName.Data
{
    /* This is used if database provider does't define
     * IProjectNameDbSchemaMigrator implementation.
     */
    public class NullProjectNameDbSchemaMigrator : IProjectNameDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
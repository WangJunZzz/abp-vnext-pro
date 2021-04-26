using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CompanyNameProjectName.Data
{
    /* This is used if database provider does't define
     * ICompanyNameProjectNameDbSchemaMigrator implementation.
     */
    public class NullCompanyNameProjectNameDbSchemaMigrator : ICompanyNameProjectNameDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
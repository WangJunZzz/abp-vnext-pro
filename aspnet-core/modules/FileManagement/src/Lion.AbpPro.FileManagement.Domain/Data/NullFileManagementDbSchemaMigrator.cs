using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Lion.AbpPro.FileManagement.Data;

public class NullFileManagementDbSchemaMigrator : IFileManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
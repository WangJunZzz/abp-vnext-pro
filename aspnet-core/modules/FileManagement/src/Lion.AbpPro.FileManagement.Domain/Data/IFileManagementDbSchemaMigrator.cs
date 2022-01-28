using System.Threading.Tasks;

namespace Lion.AbpPro.FileManagement.Data;

public interface IFileManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
using System.Threading.Tasks;

namespace Lion.AbpPro.Data
{
    public interface IAbpProDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}

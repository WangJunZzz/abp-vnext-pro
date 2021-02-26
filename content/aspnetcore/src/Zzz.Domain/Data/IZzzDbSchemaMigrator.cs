using System.Threading.Tasks;

namespace Zzz.Data
{
    public interface IZzzDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}

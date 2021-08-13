using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.DataDictionaryManagement.MongoDB
{
    [ConnectionStringName(DataDictionaryManagementDbProperties.ConnectionStringName)]
    public interface IDataDictionaryManagementMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}

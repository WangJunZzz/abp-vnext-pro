using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.QueryManagement.MongoDB
{
    [ConnectionStringName(QueryManagementDbProperties.ConnectionStringName)]
    public interface IQueryManagementMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}

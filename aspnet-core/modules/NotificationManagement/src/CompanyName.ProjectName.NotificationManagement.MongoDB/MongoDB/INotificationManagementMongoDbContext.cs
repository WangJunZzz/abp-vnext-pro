using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.NotificationManagement.MongoDB
{
    [ConnectionStringName(NotificationManagementDbProperties.ConnectionStringName)]
    public interface INotificationManagementMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}

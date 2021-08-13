using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.NotificationManagement.MongoDB
{
    [ConnectionStringName(NotificationManagementDbProperties.ConnectionStringName)]
    public class NotificationManagementMongoDbContext : AbpMongoDbContext, INotificationManagementMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureNotificationManagement();
        }
    }
}
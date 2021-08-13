using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.NotificationManagement.MongoDB
{
    public class NotificationManagementMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public NotificationManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}
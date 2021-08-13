using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.NotificationManagement.MongoDB
{
    public static class NotificationManagementMongoDbContextExtensions
    {
        public static void ConfigureNotificationManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new NotificationManagementMongoModelBuilderConfigurationOptions(
                NotificationManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}
using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.QueryManagement.MongoDB
{
    public static class QueryManagementMongoDbContextExtensions
    {
        public static void ConfigureQueryManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new QueryManagementMongoModelBuilderConfigurationOptions(
                QueryManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}
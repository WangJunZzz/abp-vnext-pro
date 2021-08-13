using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.DataDictionaryManagement.MongoDB
{
    public static class DataDictionaryManagementMongoDbContextExtensions
    {
        public static void ConfigureDataDictionaryManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DataDictionaryManagementMongoModelBuilderConfigurationOptions(
                DataDictionaryManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}
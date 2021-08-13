using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.DataDictionaryManagement.MongoDB
{
    public class DataDictionaryManagementMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public DataDictionaryManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}
using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.QueryManagement.MongoDB
{
    public class QueryManagementMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public QueryManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}
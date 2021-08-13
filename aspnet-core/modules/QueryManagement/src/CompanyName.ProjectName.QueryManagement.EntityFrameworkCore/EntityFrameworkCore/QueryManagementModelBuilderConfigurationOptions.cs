using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CompanyName.ProjectName.QueryManagement.EntityFrameworkCore
{
    public class QueryManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public QueryManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
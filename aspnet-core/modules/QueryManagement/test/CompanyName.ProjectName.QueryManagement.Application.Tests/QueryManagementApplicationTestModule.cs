using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.QueryManagement
{
    [DependsOn(
        typeof(QueryManagementApplicationModule),
        typeof(QueryManagementDomainTestModule)
        )]
    public class QueryManagementApplicationTestModule : AbpModule
    {

    }
}

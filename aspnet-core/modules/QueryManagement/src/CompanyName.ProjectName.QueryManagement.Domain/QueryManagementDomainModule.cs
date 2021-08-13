using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.QueryManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(QueryManagementDomainSharedModule)
    )]
    public class QueryManagementDomainModule : AbpModule
    {

    }
}

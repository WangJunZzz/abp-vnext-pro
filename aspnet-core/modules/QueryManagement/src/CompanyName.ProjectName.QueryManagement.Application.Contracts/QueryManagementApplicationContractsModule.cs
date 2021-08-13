using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace CompanyName.ProjectName.QueryManagement
{
    [DependsOn(
        typeof(QueryManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class QueryManagementApplicationContractsModule : AbpModule
    {

    }
}

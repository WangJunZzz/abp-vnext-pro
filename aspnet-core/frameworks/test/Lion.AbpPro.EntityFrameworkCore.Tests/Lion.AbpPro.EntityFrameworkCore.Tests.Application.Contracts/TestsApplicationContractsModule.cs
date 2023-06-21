using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Lion.AbpPro.EntityFrameworkCore.Tests;

[DependsOn(
    typeof(TestsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class TestsApplicationContractsModule : AbpModule
{

}

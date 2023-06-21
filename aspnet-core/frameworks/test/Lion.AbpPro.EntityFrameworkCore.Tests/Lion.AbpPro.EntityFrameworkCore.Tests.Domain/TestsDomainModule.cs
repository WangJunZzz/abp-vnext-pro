using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.EntityFrameworkCore.Tests;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TestsDomainSharedModule)
)]
public class TestsDomainModule : AbpModule
{

}

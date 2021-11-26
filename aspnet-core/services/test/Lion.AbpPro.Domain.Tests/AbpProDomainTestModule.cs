using Lion.AbpPro.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProEntityFrameworkCoreTestModule)
        )]
    public class AbpProDomainTestModule : AbpModule
    {

    }
}
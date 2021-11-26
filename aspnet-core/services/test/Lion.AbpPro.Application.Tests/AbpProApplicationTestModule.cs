using Volo.Abp.Modularity;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProApplicationModule),
        typeof(AbpProDomainTestModule)
        )]
    public class AbpProApplicationTestModule : AbpModule
    {

    }
}
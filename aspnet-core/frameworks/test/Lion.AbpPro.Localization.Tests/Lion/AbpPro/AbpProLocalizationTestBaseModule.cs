using Volo.Abp;
using Volo.Abp.Modularity;

namespace Lion.AbpPro
{

    [DependsOn(typeof(AbpProLocalizationModule))]
    [DependsOn(typeof(AbpTestBaseModule))]
    public class AbpProLocalizationTestBaseModule : AbpModule
    {
    }
}

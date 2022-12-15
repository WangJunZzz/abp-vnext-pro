using Volo.Abp;
using Volo.Abp.Modularity;

namespace Lion.AbpPro
{

    [DependsOn(typeof(LionAbpProLocalizationModule))]
    [DependsOn(typeof(AbpTestBaseModule))]
    public class LionAbpProLocalizationTestBaseModule : AbpModule
    {
    }
}

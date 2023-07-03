using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.Core
{
    [DependsOn(typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule))]
    public class AbpProTestBaseModule : AbpModule
    {
    }
}
using Lion.AbpPro.IdGenerator;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.Core
{
    [DependsOn(typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule),
        typeof(AbpProIdGeneratorModule))]
    public class AbpProTestBaseModule : AbpModule
    {
    }
}
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.CAP
{
    [DependsOn(typeof(AbpEventBusModule))]
    public class AbpProAbpCapModule : AbpModule
    {
    }
}
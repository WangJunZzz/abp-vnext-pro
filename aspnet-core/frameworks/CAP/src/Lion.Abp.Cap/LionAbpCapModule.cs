using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace Lion.Abp.Cap
{
    [DependsOn(typeof(AbpEventBusModule))]
    public class LionAbpCapModule : AbpModule
    {
    }
}
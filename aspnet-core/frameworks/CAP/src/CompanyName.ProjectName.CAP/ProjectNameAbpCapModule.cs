using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.CAP
{
    [DependsOn(typeof(AbpEventBusModule))]
    public class ProjectNameAbpCapModule : AbpModule
    {
    }
}
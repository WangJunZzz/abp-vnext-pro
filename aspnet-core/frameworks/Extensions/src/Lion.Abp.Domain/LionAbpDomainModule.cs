using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;

namespace Lion.Abp.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpObjectMappingModule))]
    public class LionAbpDomainModule : AbpModule
    {
    }
}
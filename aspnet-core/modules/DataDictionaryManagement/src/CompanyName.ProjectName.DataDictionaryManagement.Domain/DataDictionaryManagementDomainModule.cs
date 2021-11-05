using Lion.Abp.Domain;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.DataDictionaryManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DataDictionaryManagementDomainSharedModule),
        typeof(LionAbpDomainModule)
    )]
    public class DataDictionaryManagementDomainModule : AbpModule
    {

    }
}

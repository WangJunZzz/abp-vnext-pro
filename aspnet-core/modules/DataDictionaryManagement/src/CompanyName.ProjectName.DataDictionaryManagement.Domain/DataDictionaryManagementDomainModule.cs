using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.DataDictionaryManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DataDictionaryManagementDomainSharedModule)
    )]
    public class DataDictionaryManagementDomainModule : AbpModule
    {

    }
}

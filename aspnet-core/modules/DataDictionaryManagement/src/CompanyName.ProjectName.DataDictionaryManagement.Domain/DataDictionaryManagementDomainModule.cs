using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.DataDictionaryManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DataDictionaryManagementDomainSharedModule),
        typeof(AbpCachingModule),
        typeof(AbpAutoMapperModule)
    )]
    public class DataDictionaryManagementDomainModule : AbpModule
    {

    }
}

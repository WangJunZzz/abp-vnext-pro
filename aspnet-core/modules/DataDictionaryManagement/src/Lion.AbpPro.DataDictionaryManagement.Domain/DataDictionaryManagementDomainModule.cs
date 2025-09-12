using Volo.Abp.AutoMapper;

namespace Lion.AbpPro.DataDictionaryManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DataDictionaryManagementDomainSharedModule),
        typeof(AbpCachingModule),
        typeof(AbpAutoMapperModule)
    )]
    public class DataDictionaryManagementDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DataDictionaryManagementDomainModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<DataDictionaryManagementDomainModule>(validate: true);
            });
        }
    }
}

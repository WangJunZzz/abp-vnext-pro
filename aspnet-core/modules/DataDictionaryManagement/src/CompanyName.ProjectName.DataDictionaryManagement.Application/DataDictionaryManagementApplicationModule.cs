using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace CompanyName.ProjectName.DataDictionaryManagement
{
    [DependsOn(
        typeof(DataDictionaryManagementDomainModule),
        typeof(DataDictionaryManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class DataDictionaryManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DataDictionaryManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<DataDictionaryManagementApplicationModule>(validate: true);
            });
        }
    }
}

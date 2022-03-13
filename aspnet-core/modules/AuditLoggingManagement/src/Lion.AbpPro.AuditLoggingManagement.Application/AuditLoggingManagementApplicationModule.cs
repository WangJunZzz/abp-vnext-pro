using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AuditLogging;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.AuditLoggingManagement
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpAutofacModule),
        typeof(AuditLoggingManagementApplicationContractsModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpAutoMapperModule)
    )]
    public class AuditLoggingManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AuditLoggingManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AuditLoggingManagementApplicationModule>();
            });
        }
    }
}

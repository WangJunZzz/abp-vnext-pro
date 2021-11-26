using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Lion.AbpPro.NotificationManagement.Notifications;

namespace Lion.AbpPro.NotificationManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(NotificationManagementDomainSharedModule),
        typeof(AbpAutoMapperModule)
    )]
    public class NotificationManagementDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<NotificationManagementDomainModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<NotificationDomainAutoMapperProfile>(validate: true);
            });
        }
    }
}

using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.Local;

namespace Lion.AbpPro.DataDictionaryManagement
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(DataDictionaryManagementDomainModule)
        )]
    public class DataDictionaryManagementTestBaseModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 单元测试取消本地事件
            context.Services.Replace(ServiceDescriptor.Singleton<ILocalEventBus>(NullLocalEventBus.Instance));
            // 单元测试取消集成事件
            context.Services.Replace(ServiceDescriptor.Singleton<IDistributedEventBus>(NullDistributedEventBus.Instance));
            context.Services.AddAlwaysAllowAuthorization();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            SeedTestData(context);
        }

        private static void SeedTestData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                }
            });
        }
    }
}

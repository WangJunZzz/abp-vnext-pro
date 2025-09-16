using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.SignalR;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSignalRModule))]
public class AbpProNotificationModule : AbpModule
{
}
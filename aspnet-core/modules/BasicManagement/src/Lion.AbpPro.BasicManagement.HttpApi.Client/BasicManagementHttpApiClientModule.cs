using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Lion.AbpPro.BasicManagement;

[DependsOn(
    typeof(BasicManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class BasicManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(BasicManagementApplicationContractsModule).Assembly,
            BasicManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BasicManagementHttpApiClientModule>();
        });

    }
}

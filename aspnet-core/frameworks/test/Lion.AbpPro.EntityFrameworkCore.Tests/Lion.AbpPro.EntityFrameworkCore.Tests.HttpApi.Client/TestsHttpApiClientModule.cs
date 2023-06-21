using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Lion.AbpPro.EntityFrameworkCore.Tests;

[DependsOn(
    typeof(TestsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class TestsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(TestsApplicationContractsModule).Assembly,
            TestsRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TestsHttpApiClientModule>();
        });

    }
}

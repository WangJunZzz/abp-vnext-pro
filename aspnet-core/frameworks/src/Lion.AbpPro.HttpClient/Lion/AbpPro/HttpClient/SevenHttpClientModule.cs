using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.HttpClient;

[DependsOn(typeof(AbpAutofacModule))]
public class SevenHttpClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // 示例项目
        context.Services.AddHttpApi<IDemoHttpClient>().ConfigureHttpApi(options => { options.HttpHost = new Uri("http://localhost:5048/"); });
    }
}
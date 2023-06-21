using Localization.Resources.AbpUi;
using Lion.AbpPro.EntityFrameworkCore.Tests.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Lion.AbpPro.EntityFrameworkCore.Tests;

[DependsOn(
    typeof(TestsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class TestsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TestsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TestsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}

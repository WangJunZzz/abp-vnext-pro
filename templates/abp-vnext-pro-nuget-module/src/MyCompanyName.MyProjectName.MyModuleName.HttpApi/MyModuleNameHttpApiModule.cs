using Localization.Resources.AbpUi;
using MyCompanyName.MyProjectName.MyModuleName.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MyCompanyName.MyProjectName.MyModuleName
{
    [DependsOn(
        typeof(MyModuleNameApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class MyModuleNameHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(MyModuleNameHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<MyModuleNameResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}

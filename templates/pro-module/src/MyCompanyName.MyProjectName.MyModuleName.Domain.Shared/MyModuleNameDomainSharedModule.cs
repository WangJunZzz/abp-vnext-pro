using Lion.AbpPro.Core;

namespace MyCompanyName.MyProjectName.MyModuleName
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(AbpProCoreModule)
    )]
    public class MyModuleNameDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MyModuleNameDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<MyModuleNameResource>(MyModuleNameConsts.DefaultCultureName)
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/MyModuleName");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(MyModuleNameConsts.NameSpace, typeof(MyModuleNameResource));
            });
        }
    }
}

using Lion.AbpPro.BasicManagement;
using Lion.AbpPro.BasicManagement.Localization;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(BasicManagementDomainSharedModule),
        typeof(DataDictionaryManagementDomainSharedModule),
        typeof(FileManagementDomainSharedModule),
        typeof(NotificationManagementDomainSharedModule)
    )]
    public class AbpProDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AbpProGlobalFeatureConfigurator.Configure();
            AbpProModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProDomainSharedModule>(AbpProDomainSharedConsts.NameSpace);
            });
          
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AbpProResource>(AbpProDomainSharedConsts.DefaultCultureName)
                    .AddVirtualJson("/Localization/AbpPro")
                    .AddBaseTypes(typeof(BasicManagementResource))
                    .AddBaseTypes(typeof(AbpTimingResource));

                options.DefaultResourceType = typeof(AbpProResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(AbpProDomainSharedConsts.NameSpace, typeof(AbpProResource));
            });
        }

       
    }
}
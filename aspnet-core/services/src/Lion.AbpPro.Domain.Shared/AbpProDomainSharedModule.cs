namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpTenantManagementDomainSharedModule),
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
                    .AddBaseTypes(typeof(IdentityResource))
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddBaseTypes(typeof(AbpLocalizationResource))
                    .AddBaseTypes(typeof(AbpTimingResource))
                    .AddBaseTypes(typeof(AbpSettingManagementResource));

                options.DefaultResourceType = typeof(AbpProResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(AbpProDomainSharedConsts.NameSpace, typeof(AbpProResource));
            });
        }

       
    }
}
namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(BasicManagementDomainSharedModule),
        typeof(NotificationManagementDomainSharedModule),
        typeof(DataDictionaryManagementDomainSharedModule),
        typeof(LanguageManagementDomainSharedModule),
        typeof(AbpProCoreModule)
    )]
    public class MyProjectNameDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            MyProjectNameGlobalFeatureConfigurator.Configure();
            MyProjectNameModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MyProjectNameDomainSharedModule>(MyProjectNameDomainSharedConsts.NameSpace);
            });
          
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<MyProjectNameResource>(MyProjectNameDomainSharedConsts.DefaultCultureName)
                    .AddVirtualJson("/Localization/MyProjectName")
                    .AddBaseTypes(typeof(BasicManagementResource))
                    .AddBaseTypes(typeof(AbpTimingResource));

                options.DefaultResourceType = typeof(MyProjectNameResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(MyProjectNameDomainSharedConsts.NameSpace, typeof(MyProjectNameResource));
            });
        }

       
    }
}
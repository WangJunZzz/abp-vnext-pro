﻿using Lion.AbpPro.Core;

namespace Lion.AbpPro.BasicManagement;

[DependsOn(
    typeof(AbpAuditLoggingDomainSharedModule),
    typeof(AbpBackgroundJobsDomainSharedModule),
    typeof(AbpFeatureManagementDomainSharedModule),
    typeof(AbpIdentityDomainSharedModule),
    typeof(AbpPermissionManagementDomainSharedModule),
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(AbpTenantManagementDomainSharedModule),
    typeof(AbpProCoreModule)
)]
public class BasicManagementDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BasicManagementDomainSharedModule>(BasicManagementConsts.NameSpace);
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<BasicManagementResource>(BasicManagementConsts.DefaultCultureName)
                .AddVirtualJson("/Localization/BasicManagement");

            options.DefaultResourceType = typeof(BasicManagementResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(BasicManagementConsts.NameSpace, typeof(BasicManagementResource));
        });
    }
}

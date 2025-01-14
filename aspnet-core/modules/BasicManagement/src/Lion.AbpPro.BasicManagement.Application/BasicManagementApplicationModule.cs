using Lion.AbpPro.BasicManagement.ConfigurationOptions;
using Lion.AbpPro.BasicManagement.Roles;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Lion.AbpPro.BasicManagement;

[DependsOn(
    typeof(BasicManagementDomainModule),
    typeof(BasicManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpIdentityAspNetCoreModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpAuditLoggingDomainModule)
    )]
public class BasicManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<BasicManagementApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BasicManagementApplicationModule>(validate: true);
        });
        
        
        Configure<PermissionOptions>(options =>
        {
            options.Excludes.Add(IdentityPermissions.Users.ManagePermissions);
            options.Excludes.Add(IdentityPermissions.Users.ManageRoles);
            options.Excludes.Add(IdentityPermissions.UserLookup.Default);
            options.Excludes.Add(FeatureManagementPermissions.GroupName);
            options.Excludes.Add(FeatureManagementPermissions.ManageHostFeatures);
            options.Excludes.Add(SettingManagementPermissions.GroupName);
            options.Excludes.Add(SettingManagementPermissions.Emailing);
            options.Excludes.Add(TenantManagementPermissions.Tenants.ManageFeatures);
            //options.Excludes.Add(TenantManagementPermissions.Tenants.ManageConnectionStrings);
        });
        
        context.Services.Configure<JwtOptions>(context.Services.GetConfiguration().GetSection("Jwt"));

        ConfigureMagicodes(context);
    }
    
    /// <summary>
    /// 配置Magicodes.IE
    /// Excel导入导出
    /// </summary>
    private void ConfigureMagicodes(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IExporter, ExcelExporter>();
        context.Services.AddTransient<IExcelExporter, ExcelExporter>();
    }
}

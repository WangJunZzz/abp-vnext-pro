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
            options.Excludes.Add("AbpIdentity.Users.ManagePermissions");
            options.Excludes.Add("AbpIdentity.UserLookup");
            options.Excludes.Add("FeatureManagement");
            options.Excludes.Add("FeatureManagement.ManageHostFeatures");
            options.Excludes.Add("SettingManagement");
            options.Excludes.Add("SettingManagement.Emailing");
            options.Excludes.Add("AbpTenantManagement");
            options.Excludes.Add("AbpTenantManagement.Tenants.ManageFeatures");
            options.Excludes.Add("AbpTenantManagement.Tenants.ManageConnectionStrings");
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

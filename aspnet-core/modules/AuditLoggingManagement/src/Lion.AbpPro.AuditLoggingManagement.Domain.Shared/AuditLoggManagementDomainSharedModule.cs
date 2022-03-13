using Lion.AbpPro.AuditLoggingManagement.Domain.Shared.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Localization.Resources.AbpLocalization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Lion.AbpPro.AuditLoggingManagement.Domain.Shared
{
    [DependsOn(
       typeof(AbpValidationModule),
       typeof(AbpAuditLoggingDomainSharedModule)
    )]
    public class AuditLoggManagementDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AuditLoggManagementDomainSharedModule>(typeof(AuditLoggManagementDomainSharedModule).Namespace);
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AuditLogManagementResource>("zh-Hans")
                    .AddVirtualJson("/Localization/AuditLogManagement");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("AuditLogManagement", typeof(AuditLogManagementResource));
            });
        }
    }
}
namespace Lion.AbpPro.BasicManagement;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(BasicManagementDomainSharedModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpBackgroundJobsDomainModule),
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpTenantManagementDomainModule)
)]
public class BasicManagementDomainModule : AbpModule
{
}

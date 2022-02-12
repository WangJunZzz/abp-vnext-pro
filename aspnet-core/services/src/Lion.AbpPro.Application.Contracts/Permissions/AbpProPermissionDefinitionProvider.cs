using Lion.AbpPro.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Lion.AbpPro.Permissions
{
    public class AbpProPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var abpIdentityGroup = context.GetGroup(IdentityPermissions.GroupName);
            var userManagement = abpIdentityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
            userManagement.AddChild(AbpProPermissions.SystemManagement.UserEnable, L("Permission:Enable"));
            userManagement.AddChild(AbpProPermissions.SystemManagement.UserExport, L("Permission:Export"));

            var auditManagement =
                abpIdentityGroup.AddPermission(AbpProPermissions.SystemManagement.AuditLog, L("Permission:AuditLogManagement"));
            var esManagement = abpIdentityGroup.AddPermission(AbpProPermissions.SystemManagement.ES, L("Permission:ESManagement"));
            var settingManagement = abpIdentityGroup.AddPermission(AbpProPermissions.SystemManagement.Setting, L("Permission:SettingManagement"));

            #region IdentityServer

            // multiTenancySide: MultiTenancySides.Host 只有host租户才有权限
            var identityServerManagementGroup =
                context.AddGroup(AbpProPermissions.IdentityServer.IdentityServerManagement, L("Permission:IdentityServerManagement"),
                    multiTenancySide: MultiTenancySides.Host);

            var clientManagment = identityServerManagementGroup.AddPermission(AbpProPermissions.IdentityServer.Client.Default,
                L("Permission:IdentityServerManagement:Client"),multiTenancySide: MultiTenancySides.Host);
            clientManagment.AddChild(AbpProPermissions.IdentityServer.Client.Create,
                L("Permission:Create"),multiTenancySide: MultiTenancySides.Host);
            clientManagment.AddChild(AbpProPermissions.IdentityServer.Client.Update,
                L("Permission:Update"),multiTenancySide: MultiTenancySides.Host);
            clientManagment.AddChild(AbpProPermissions.IdentityServer.Client.Delete,
                L("Permission:Delete"),multiTenancySide: MultiTenancySides.Host);
            clientManagment.AddChild(AbpProPermissions.IdentityServer.Client.Enable,
                L("Permission:Enable"),multiTenancySide: MultiTenancySides.Host);


            var apiResourceManagment = identityServerManagementGroup.AddPermission(
                AbpProPermissions.IdentityServer.ApiResource.Default,
                L("Permission:IdentityServerManagement:ApiResource"),multiTenancySide: MultiTenancySides.Host);
            apiResourceManagment.AddChild(AbpProPermissions.IdentityServer.ApiResource.Create,
                L("Permission:Create"),multiTenancySide: MultiTenancySides.Host);
            apiResourceManagment.AddChild(AbpProPermissions.IdentityServer.ApiResource.Update,
                L("Permission:Update"),multiTenancySide: MultiTenancySides.Host);
            apiResourceManagment.AddChild(AbpProPermissions.IdentityServer.ApiResource.Delete,
                L("Permission:Delete"),multiTenancySide: MultiTenancySides.Host);

            var apiScopeManagment = identityServerManagementGroup.AddPermission(AbpProPermissions.IdentityServer.ApiScope.Default,
                L("Permission:IdentityServerManagement:ApiScope"),multiTenancySide: MultiTenancySides.Host);
            apiScopeManagment.AddChild(AbpProPermissions.IdentityServer.ApiScope.Create,
                L("Permission:Create"),multiTenancySide: MultiTenancySides.Host);
            apiScopeManagment.AddChild(AbpProPermissions.IdentityServer.ApiScope.Update,
                L("Permission:Update"),multiTenancySide: MultiTenancySides.Host);
            apiScopeManagment.AddChild(AbpProPermissions.IdentityServer.ApiScope.Delete,
                L("Permission:Delete"),multiTenancySide: MultiTenancySides.Host);


            var identityResourcesManagment = identityServerManagementGroup.AddPermission(
                AbpProPermissions.IdentityServer.IdentityResources.Default,
                L("Permission:IdentityServerManagement:IdentityResources"),multiTenancySide: MultiTenancySides.Host);
            identityResourcesManagment.AddChild(AbpProPermissions.IdentityServer.IdentityResources.Create,
                L("Permission:Create"),multiTenancySide: MultiTenancySides.Host);
            identityResourcesManagment.AddChild(AbpProPermissions.IdentityServer.IdentityResources.Update,
                L("Permission:Update"),multiTenancySide: MultiTenancySides.Host);
            identityResourcesManagment.AddChild(AbpProPermissions.IdentityServer.IdentityResources.Delete,
                L("Permission:Delete"),multiTenancySide: MultiTenancySides.Host);

            #endregion
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpProResource>(name);
        }
    }
}
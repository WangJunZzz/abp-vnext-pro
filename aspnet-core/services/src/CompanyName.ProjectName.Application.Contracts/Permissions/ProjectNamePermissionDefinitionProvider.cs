using CompanyName.ProjectName.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace CompanyName.ProjectName.Permissions
{
    public class ProjectNamePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var abpIdentityGroup = context.GetGroup(IdentityPermissions.GroupName);
            var userManagement = abpIdentityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
            userManagement.AddChild(ProjectNamePermissions.SystemManagement.UserEnable, L("Permission:Enable"));

            var auditManagement =
                abpIdentityGroup.AddPermission(ProjectNamePermissions.SystemManagement.AuditLog, L("Permission:AuditLogManagement"));

          
            var esManagement = abpIdentityGroup.AddPermission(ProjectNamePermissions.SystemManagement.ES, L("Permission:ESManagement"));

            #region IdentityServer

            // multiTenancySide: MultiTenancySides.Host 只有host租户才有权限
            var identityServerManagementGroup =
                context.AddGroup(ProjectNamePermissions.IdentityServer.IdentityServerManagement, L("Permission:IdentityServerManagement"),
                    multiTenancySide: MultiTenancySides.Host);

            var clientManagment = identityServerManagementGroup.AddPermission(ProjectNamePermissions.IdentityServer.Client.Default,
                L("Permission:IdentityServerManagement:Client"),multiTenancySide: MultiTenancySides.Host);
            clientManagment.AddChild(ProjectNamePermissions.IdentityServer.Client.Create,
                L("Permission:Create"),multiTenancySide: MultiTenancySides.Host);
            clientManagment.AddChild(ProjectNamePermissions.IdentityServer.Client.Update,
                L("Permission:Update"),multiTenancySide: MultiTenancySides.Host);
            clientManagment.AddChild(ProjectNamePermissions.IdentityServer.Client.Delete,
                L("Permission:Delete"),multiTenancySide: MultiTenancySides.Host);
            clientManagment.AddChild(ProjectNamePermissions.IdentityServer.Client.Enable,
                L("Permission:Enable"),multiTenancySide: MultiTenancySides.Host);


            var apiResourceManagment = identityServerManagementGroup.AddPermission(
                ProjectNamePermissions.IdentityServer.ApiResource.Default,
                L("Permission:IdentityServerManagement:ApiResource"),multiTenancySide: MultiTenancySides.Host);
            apiResourceManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiResource.Create,
                L("Permission:Create"),multiTenancySide: MultiTenancySides.Host);
            apiResourceManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiResource.Update,
                L("Permission:Update"),multiTenancySide: MultiTenancySides.Host);
            apiResourceManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiResource.Delete,
                L("Permission:Delete"),multiTenancySide: MultiTenancySides.Host);

            var apiScopeManagment = identityServerManagementGroup.AddPermission(ProjectNamePermissions.IdentityServer.ApiScope.Default,
                L("Permission:IdentityServerManagement:ApiScope"),multiTenancySide: MultiTenancySides.Host);
            apiScopeManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiScope.Create,
                L("Permission:Create"),multiTenancySide: MultiTenancySides.Host);
            apiScopeManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiScope.Update,
                L("Permission:Update"),multiTenancySide: MultiTenancySides.Host);
            apiScopeManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiScope.Delete,
                L("Permission:Delete"),multiTenancySide: MultiTenancySides.Host);


            var identityResourcesManagment = identityServerManagementGroup.AddPermission(
                ProjectNamePermissions.IdentityServer.IdentityResources.Default,
                L("Permission:IdentityServerManagement:IdentityResources"),multiTenancySide: MultiTenancySides.Host);
            identityResourcesManagment.AddChild(ProjectNamePermissions.IdentityServer.IdentityResources.Create,
                L("Permission:Create"),multiTenancySide: MultiTenancySides.Host);
            identityResourcesManagment.AddChild(ProjectNamePermissions.IdentityServer.IdentityResources.Update,
                L("Permission:Update"),multiTenancySide: MultiTenancySides.Host);
            identityResourcesManagment.AddChild(ProjectNamePermissions.IdentityServer.IdentityResources.Delete,
                L("Permission:Delete"),multiTenancySide: MultiTenancySides.Host);

            #endregion
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProjectNameResource>(name);
        }
    }
}
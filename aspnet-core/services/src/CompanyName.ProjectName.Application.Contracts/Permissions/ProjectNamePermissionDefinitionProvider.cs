using CompanyName.ProjectName.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;

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

            var hangfireManagement =
                abpIdentityGroup.AddPermission(ProjectNamePermissions.SystemManagement.Hangfire, L("Permission:HangfireManagement"));

            var capManagement = abpIdentityGroup.AddPermission(ProjectNamePermissions.SystemManagement.Cap, L("Permission:CapManagement"));
            
           

            #region IdentityServer

            var identityServerManagementGroup =
                context.AddGroup(ProjectNamePermissions.IdentityServer.IdentityServerManagement, L("Permission:IdentityServerManagement"));

            var clientManagment = identityServerManagementGroup.AddPermission(ProjectNamePermissions.IdentityServer.Client.Default,
                L("Permission:IdentityServerManagement:Client"));
            clientManagment.AddChild(ProjectNamePermissions.IdentityServer.Client.Create,
                L("Permission:Create"));
            clientManagment.AddChild(ProjectNamePermissions.IdentityServer.Client.Update,
                L("Permission:Update"));
            clientManagment.AddChild(ProjectNamePermissions.IdentityServer.Client.Delete,
                L("Permission:Delete"));
            clientManagment.AddChild(ProjectNamePermissions.IdentityServer.Client.Enable,
                L("Permission:Enable"));
            
            
            var apiResourceManagment = identityServerManagementGroup.AddPermission(ProjectNamePermissions.IdentityServer.ApiResource.Default,
                L("Permission:IdentityServerManagement:ApiResource"));
            apiResourceManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiResource.Create,
                L("Permission:Create"));
            apiResourceManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiResource.Update,
                L("Permission:Update"));
            apiResourceManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiResource.Delete,
                L("Permission:Delete"));

            var apiScopeManagment = identityServerManagementGroup.AddPermission(ProjectNamePermissions.IdentityServer.ApiScope.Default,
                L("Permission:IdentityServerManagement:ApiScope"));
            apiScopeManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiScope.Create,
                L("Permission:Create"));
            apiScopeManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiScope.Update,
                L("Permission:Update"));
            apiScopeManagment.AddChild(ProjectNamePermissions.IdentityServer.ApiScope.Delete,
                L("Permission:Delete"));
            

            var identityResourcesManagment = identityServerManagementGroup.AddPermission(ProjectNamePermissions.IdentityServer.IdentityResources.Default,
                L("Permission:IdentityServerManagement:IdentityResources"));
            identityResourcesManagment.AddChild(ProjectNamePermissions.IdentityServer.IdentityResources.Create,
                L("Permission:Create"));
            identityResourcesManagment.AddChild(ProjectNamePermissions.IdentityServer.IdentityResources.Update,
                L("Permission:Update"));
            identityResourcesManagment.AddChild(ProjectNamePermissions.IdentityServer.IdentityResources.Delete,
                L("Permission:Delete"));
            
            #endregion
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProjectNameResource>(name);
        }
    }
}
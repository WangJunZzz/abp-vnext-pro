using CompanyNameProjectName.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CompanyNameProjectName.Permissions
{
    public class CompanyNameProjectNamePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var abpIdentityGroup = context.GetGroup("AbpIdentity");
            var userManagement = abpIdentityGroup.GetPermissionOrNull("AbpIdentity.Users");

            userManagement.AddChild("AbpIdentity.Users.Lock", L("Permission:Users:Enable"));
            userManagement.AddChild("AbpIdentity.Users.Query", L("Permission:Query"));

            var roleManagement = abpIdentityGroup.GetPermissionOrNull("AbpIdentity.Roles");
            roleManagement.AddChild("AbpIdentity.Roles.Query", L("Permission:Query"));

            var auditManagement = abpIdentityGroup.AddPermission("AbpIdentity.AuditLog", L("Permission:AuditLogMangament"));
            auditManagement.AddChild("AbpIdentity.AuditLog.Query", L("Permission:Query"));

        
        

            //abpIdentityGroup.AddPermission("AbpIdentity.Users.Lock", L("Permission:Users:Enable"));
            //abpIdentityGroup.AddPermission("AbpIdentity.Users.AuditLog", L("Permission:Users:AuditLog"));
            //Define your own permissions here. Example:
            //myGroup.AddPermission(CompanyNameProjectNamePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CompanyNameProjectNameResource>(name);
        }
    }
}

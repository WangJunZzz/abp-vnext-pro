using CompanyName.ProjectName.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CompanyName.ProjectName.Permissions
{
    public class ProjectNamePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            
            var abpIdentityGroup = context.GetGroup(ProjectNamePermissions.AbpIdentityGroupName);
            var userManagement = abpIdentityGroup.GetPermissionOrNull(ProjectNamePermissions.AbpIdentityExtend.Users);

            userManagement.AddChild(ProjectNamePermissions.AbpIdentityExtend.UserEnable, L("Permissions:Enable"));
            userManagement.AddChild(ProjectNamePermissions.AbpIdentityExtend.UserQuery, L("Permissions:Query"));

            var roleManagement = abpIdentityGroup.GetPermissionOrNull(ProjectNamePermissions.AbpIdentityExtend.Roles);
            roleManagement.AddChild(ProjectNamePermissions.AbpIdentityExtend.RoleQuery, L("Permissions:Query"));

            var auditManagement = abpIdentityGroup.AddPermission(ProjectNamePermissions.AbpIdentityExtend.AuditLogs, L("Permissions:AuditLogManagement"));
            auditManagement.AddChild(ProjectNamePermissions.AbpIdentityExtend.AuditLogQuery, L("Permissions:Query"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProjectNameResource>(name);
        }
    }
}

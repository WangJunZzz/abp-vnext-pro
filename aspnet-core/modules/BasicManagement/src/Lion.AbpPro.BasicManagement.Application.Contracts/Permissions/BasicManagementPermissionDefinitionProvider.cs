using Lion.AbpPro.BasicManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;

namespace Lion.AbpPro.BasicManagement.Permissions;

public class BasicManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var abpIdentityGroup = context.GetGroup(IdentityPermissions.GroupName);
        var userManagement = abpIdentityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
        userManagement.AddChild(BasicManagementPermissions.SystemManagement.UserEnable, L("Permission:Enable"));
        userManagement.AddChild(BasicManagementPermissions.SystemManagement.UserExport, L("Permission:Export"));

        var auditManagement =
            abpIdentityGroup.AddPermission(BasicManagementPermissions.SystemManagement.AuditLog, L("Permission:AuditLogManagement"));
        var settingManagement = abpIdentityGroup.AddPermission(BasicManagementPermissions.SystemManagement.Setting, L("Permission:SettingManagement"));
        var organizationUnitManagement = abpIdentityGroup.AddPermission(BasicManagementPermissions.SystemManagement.OrganizationUnit, L("Permission:OrganizationUnitManagement"));
        organizationUnitManagement.AddChild
        (
            BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Create,
            L("Permission:Create")
        );
        organizationUnitManagement.AddChild
        (
            BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Update,
            L("Permission:Update")
        );
        organizationUnitManagement.AddChild
        (
            BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Delete,
            L("Permission:Delete")
        );

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BasicManagementResource>(name);
    }
}

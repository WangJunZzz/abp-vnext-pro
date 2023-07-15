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
        userManagement.AddChild(BasicManagementPermissions.SystemManagement.UserEnable, L("Permission:Enable"), multiTenancySide: MultiTenancySides.Both);
        userManagement.AddChild(BasicManagementPermissions.SystemManagement.UserExport, L("Permission:Export"), multiTenancySide: MultiTenancySides.Both);

        var auditManagement =
            abpIdentityGroup.AddPermission(BasicManagementPermissions.SystemManagement.AuditLog, L("Permission:AuditLogManagement"), multiTenancySide: MultiTenancySides.Both);
        var settingManagement = abpIdentityGroup.AddPermission(BasicManagementPermissions.SystemManagement.Setting, L("Permission:SettingManagement"), multiTenancySide: MultiTenancySides.Both);
        var organizationUnitManagement = abpIdentityGroup.AddPermission(BasicManagementPermissions.SystemManagement.OrganizationUnit, L("Permission:OrganizationUnitManagement"), multiTenancySide: MultiTenancySides.Both);
        organizationUnitManagement.AddChild
        (
            BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Create,
            L("Permission:Create"), multiTenancySide: MultiTenancySides.Both
        );
        organizationUnitManagement.AddChild
        (
            BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Update,
            L("Permission:Update"), multiTenancySide: MultiTenancySides.Both
        );
        organizationUnitManagement.AddChild
        (
            BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Delete,
            L("Permission:Delete"), multiTenancySide: MultiTenancySides.Both
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BasicManagementResource>(name);
    }
}
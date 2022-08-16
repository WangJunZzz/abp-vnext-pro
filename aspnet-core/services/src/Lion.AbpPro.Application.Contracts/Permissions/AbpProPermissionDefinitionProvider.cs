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
            var organizationUnitManagement = abpIdentityGroup.AddPermission(AbpProPermissions.SystemManagement.OrganizationUnit, L("Permission:OrganizationUnitManagement"));
            organizationUnitManagement.AddChild
            (
                AbpProPermissions.SystemManagement.OrganizationUnitManagement.Create,
                L("Permission:Create")
            );
            organizationUnitManagement.AddChild
            (
                AbpProPermissions.SystemManagement.OrganizationUnitManagement.Update,
                L("Permission:Update")
            );
            organizationUnitManagement.AddChild
            (
                AbpProPermissions.SystemManagement.OrganizationUnitManagement.Delete,
                L("Permission:Delete")
            );

       
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpProResource>(name);
        }
    }
}
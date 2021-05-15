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
            abpIdentityGroup.AddPermission("AbpIdentity.Users.Lock", L("Permission:Users:Enable"));
            abpIdentityGroup.AddPermission("AbpIdentity.Users.AuditLog", L("Permission:Users:AuditLog"));
            //Define your own permissions here. Example:
            //myGroup.AddPermission(CompanyNameProjectNamePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CompanyNameProjectNameResource>(name);
        }
    }
}

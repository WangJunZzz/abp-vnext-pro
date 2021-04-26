using CompanyNameProjectName.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CompanyNameProjectName.Permissions
{
    public class CompanyNameProjectNamePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {


            //Define your own permissions here. Example:
            //myGroup.AddPermission(CompanyNameProjectNamePermissions.MyPermission1, L("Permission:MyPermission1"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CompanyNameProjectNameResource>(name);
        }
    }
}

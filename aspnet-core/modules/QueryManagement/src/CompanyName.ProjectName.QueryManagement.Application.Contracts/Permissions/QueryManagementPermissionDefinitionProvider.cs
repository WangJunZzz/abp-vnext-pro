using CompanyName.ProjectName.QueryManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;

namespace CompanyName.ProjectName.QueryManagement.Permissions
{
    public class QueryManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var abpIdentityGroup = context.GetGroup(IdentityPermissions.GroupName);
            var esManagement = abpIdentityGroup.AddPermission(QueryManagementPermissions.SystemManagement.ES, L("Permission:ESManagement"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<QueryManagementResource>(name);
        }
    }
}
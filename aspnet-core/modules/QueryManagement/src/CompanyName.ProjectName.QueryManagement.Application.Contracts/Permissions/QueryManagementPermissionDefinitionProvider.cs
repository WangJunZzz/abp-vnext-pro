using CompanyName.ProjectName.QueryManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CompanyName.ProjectName.QueryManagement.Permissions
{
    public class QueryManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(QueryManagementPermissions.GroupName, L("Permission:QueryManagement"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<QueryManagementResource>(name);
        }
    }
}
using CompanyName.ProjectName.NotificationManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CompanyName.ProjectName.NotificationManagement.Permissions
{
    public class NotificationManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(NotificationManagementPermissions.GroupName, L("Permission:NotificationManagement"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NotificationManagementResource>(name);
        }
    }
}
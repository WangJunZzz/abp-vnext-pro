namespace Lion.AbpPro.NotificationManagement.Permissions
{
    public class NotificationManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var abpIdentityGroup = context.GetGroup("AbpIdentity");
            var notificationManagement = abpIdentityGroup.AddPermission(NotificationManagementPermissions.NotificationManagement.Default, L("Permission:NotificationManagement"));
            var notificationSubscriptionManagement = abpIdentityGroup.AddPermission(NotificationManagementPermissions.NotificationSubscriptionManagement.Default, L("Permission:NotificationSubscriptionManagement"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NotificationManagementResource>(name);
        }
    }
}
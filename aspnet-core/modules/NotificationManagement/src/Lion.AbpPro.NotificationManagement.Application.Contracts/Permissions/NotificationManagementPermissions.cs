namespace Lion.AbpPro.NotificationManagement.Permissions
{
    public class NotificationManagementPermissions
    {

        public const  string GroupName = "AbpIdentity";

        public static class NotificationManagement
        {
            public const string Default = GroupName + ".NotificationManagement";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
        
        public static class NotificationSubscriptionManagement
        {
            public const string Default = GroupName + ".NotificationSubscriptionManagement";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
        
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(NotificationManagementPermissions));
        }
    }
}
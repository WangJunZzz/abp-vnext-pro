using Volo.Abp.Reflection;

namespace CompanyName.ProjectName.NotificationManagement.Permissions
{
    public class NotificationManagementPermissions
    {
        public const string GroupName = "NotificationManagement";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(NotificationManagementPermissions));
        }
    }
}
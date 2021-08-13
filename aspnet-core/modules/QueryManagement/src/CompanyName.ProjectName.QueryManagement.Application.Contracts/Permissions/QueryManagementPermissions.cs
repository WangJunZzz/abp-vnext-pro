using Volo.Abp.Reflection;

namespace CompanyName.ProjectName.QueryManagement.Permissions
{
    public class QueryManagementPermissions
    {
        public const string GroupName = "QueryManagement";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(QueryManagementPermissions));
        }
    }
}
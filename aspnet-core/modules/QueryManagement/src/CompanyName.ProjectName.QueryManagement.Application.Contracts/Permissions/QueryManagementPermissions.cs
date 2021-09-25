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
        
        /// <summary>
        /// 系统管理扩展权限
        /// </summary>
        public static class SystemManagement
        {
            public const string Default = "System";
            public const string ES = Default + ".ES";
        }
    }
}
using Volo.Abp.Reflection;

namespace CompanyName.ProjectName.DataDictionaryManagement.Permissions
{
    public class DataDictionaryManagementPermissions
    {
        public const string GroupName = "DataDictionaryManagement";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(DataDictionaryManagementPermissions));
        }
    }
}
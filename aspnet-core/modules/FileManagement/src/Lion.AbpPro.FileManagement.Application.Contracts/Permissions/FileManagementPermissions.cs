namespace Lion.AbpPro.FileManagement.Permissions;

public class FileManagementPermissions
{
    public const  string GroupName = "AbpIdentity";

    public static class FileManagement
    {
        public const string Default = GroupName + ".FileManagement";
        public const string Upload = Default + ".Upload";
        public const string Down = Default + ".Down";
    }
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FileManagementPermissions));
    }
}
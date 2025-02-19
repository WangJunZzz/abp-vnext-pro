namespace Lion.AbpPro.FileManagement.Permissions;

public class FileManagementPermissions
{
    public const string GroupName = "FileManagement";

    public static class FileManagement
    {
        public const string Default = GroupName + ".File";
        public const string Upload = Default + ".Upload";
        public const string Download = Default + ".Download";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FileManagementPermissions));
    }
}
using Volo.Abp.Reflection;

namespace Lion.AbpPro.BasicManagement.Permissions;

public class BasicManagementPermissions
{
    /// <summary>
    /// 系统管理扩展权限
    /// </summary>
    public static class SystemManagement
    {
        public const string Default = "AbpIdentity";
        public const string UserEnable = Default + ".Users.Enable";
        public const string UserExport = Default + ".Users.Export";
        public const string AuditLog = Default + ".AuditLog";
        public const string Setting = Default + ".Setting";
        public const string OrganizationUnit = Default + ".OrganizationUnitManagement";
        public static class OrganizationUnitManagement
        {
            public const string Default = SystemManagement.Default + ".OrganizationUnitManagement";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(BasicManagementPermissions));
    }
}

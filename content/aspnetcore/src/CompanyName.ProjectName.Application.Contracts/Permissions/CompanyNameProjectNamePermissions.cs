namespace CompanyNameProjectName.Permissions
{
    public static class CompanyNameProjectNamePermissions
    {
        public const string GroupName = "CompanyNameProjectName";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";

        public static class AuditLog
        {
            public const string Default = "AuditLog";
            public const string AuditLogManagement = "AuditLog.AuditLogManagement";
            public const string Query = "AuditLog.AuditLogManagement.Query";
        }
    }
}
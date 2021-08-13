namespace CompanyName.ProjectName.Permissions
{
    public static class ProjectNamePermissions
    {
        public const string GroupName = "ProjectName";

        public const string AbpIdentityGroupName = "AbpIdentity";
        
        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
        
        /// <summary>
        /// 系统管理扩展权限
        /// </summary>
        public static class AbpIdentityExtend
        {
            public const string Default = "AbpIdentity";
            public const string Users = Default + ".Users";
            public const string Roles = Default + ".Roles";
            public const string AuditLogs = Default + ".AuditLogs";
            public const string UserEnable = Users + ".Users.Enable";
            public const string UserQuery = Users + ".Query";
            public const string RoleQuery = Roles + ".Query";
            public const string AuditLogQuery = AuditLogs + "AuditLog";
        }
    }
}
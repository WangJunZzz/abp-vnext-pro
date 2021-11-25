using CompanyName.ProjectName.IdentityServers.Clients;

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
        public static class SystemManagement
        {
            public const string Default = "System";
            public const string UserEnable = Default + ".Users.Enable";
            public const string AuditLog = Default + ".AuditLog";
            public const string ES = Default + ".ES";
            public const string Setting = Default + ".Setting";
        }


        public static class IdentityServer
        {
            public const string IdentityServerManagement = "IdentityServerManagement";


            public static class Client
            {
                public const string Default = IdentityServerManagement + ".Client";
                public const string Create = Default + ".Create";
                public const string Update = Default + ".Update";
                public const string Delete = Default + ".Delete";
                public const string Enable = Default + ".Enable";
            }


            public static class ApiResource
            {
                public const string Default = IdentityServerManagement + ".ApiResource";
                public const string Create = Default + ".Create";
                public const string Update = Default + ".Update";
                public const string Delete = Default + ".Delete";
            }
            
            public static class ApiScope
            {
                public const string Default = IdentityServerManagement + ".ApiScope";
                public const string Create = Default + ".Create";
                public const string Update = Default + ".Update";
                public const string Delete = Default + ".Delete";
            }

            public static class IdentityResources
            {
                public const string Default = IdentityServerManagement + ".IdentityResources";
                public const string Create = Default + ".Create";
                public const string Update = Default + ".Update";
                public const string Delete = Default + ".Delete";
            }
          
        }
    }
}
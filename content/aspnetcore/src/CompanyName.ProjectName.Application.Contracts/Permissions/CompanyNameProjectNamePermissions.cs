namespace CompanyNameProjectName.Permissions
{
    public static class CompanyNameProjectNamePermissions
    {
        public const string GroupName = "CompanyNameProjectName";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";

        public const string DicGroupName = "Dic";

        /// <summary>
        /// 字典权限
        /// </summary>
        public static class Dic
        {
            public const string Default = GroupName + ".Dic";
            public const string Query = Default + ".Query";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
    }
}
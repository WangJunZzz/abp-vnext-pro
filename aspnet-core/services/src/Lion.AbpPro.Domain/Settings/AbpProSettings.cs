namespace Lion.AbpPro.Settings
{
    public static class AbpProSettings
    {
        public const string Prefix = "setting_";

        /// <summary>
        /// 前端控件类型
        /// </summary>
        public static class ControlType
        {
            public const string Defalut = "Type";
            public const string TypeText = "Text";
            public const string TypeCheckBox = "CheckBox";
        }

        /// <summary>
        /// 系统控制分组
        /// </summary>
        public static class Group
        {
            public const string Defalut = "Setting.Group";
            public const string SystemManagement = Defalut + ".System";
            public const string OtherManagement = Defalut + ".Other";
        }

        /// <summary>
        /// 其他控制分组
        /// </summary>
        public static class Other
        {
            private const string Defalut = "Setting.Group.Other";
            public const string Github = Defalut + ".Github";
        }
    }
}
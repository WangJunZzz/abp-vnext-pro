namespace Lion.AbpPro.BasicManagement.Settings;

public static class BasicManagementSettings
{
    public const string Prefix = "setting_";

    /// <summary>
    /// 前端控件类型
    /// </summary>
    public static class ControlType
    {
        public const string Default = "Type";
        public const string TypeText = "Text";
        public const string TypeCheckBox = "CheckBox";
        public const string Number = "Number";
    }

    /// <summary>
    /// 系统控制分组
    /// </summary>
    public static class Group
    {
        public const string Default = "Setting.Group";
        public const string SystemManagement = Default + ".System";
    }
}

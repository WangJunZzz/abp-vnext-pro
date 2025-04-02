namespace Lion.AbpPro.BasicManagement.Settings;

public static class BasicManagementSettings
{

    /// <summary>
    /// 新用户首次登录是否强制修改密码
    /// </summary>
    public const string EnableNewAccountRequiredChangePassword = BasicManagementConsts.EnableNewAccountRequiredChangePassword;
    
    /// <summary>
    /// 启用密码过期是否强制修改密码
    /// </summary>
    public const string EnableExpireRequiredChangePassword = BasicManagementConsts.EnableExpireRequiredChangePassword;
    
    /// <summary>
    /// 定期修改密码天数默认90天
    /// </summary>
    public const string PasswordExpireDay = BasicManagementConsts.PasswordExpireDay;
    
    /// <summary>
    /// 密码过期，提前通知天数
    /// </summary>
    public const string PasswordRemindDay = BasicManagementConsts.PasswordRemindDay;
    
    /// <summary>
    /// 系统控制分组
    /// </summary>
    public static class Group
    {
        public const string Default = "Setting.Group";
        public const string SystemManagement = Default + ".System";
    }
}

namespace Lion.AbpPro.BasicManagement;

public static class BasicManagementConsts
{
    /// <summary>名称空间</summary>
    public const string NameSpace = "Lion.AbpPro.BasicManagement";
    /// <summary>默认语言</summary>
    public const string DefaultCultureName = "zh-Hans";
    
    
    /// <summary>
    /// 新用户首次登录是否强制修改密码
    /// </summary>
    public const string EnableNewAccountRequiredChangePassword = "EnableNewAccountRequiredChangePassword";
    
    /// <summary>
    /// 启用密码过期是否强制修改密码
    /// </summary>
    public const string EnableExpireRequiredChangePassword = "EnableExpireRequiredChangePassword";
    
    /// <summary>
    /// 密码过期天数
    /// </summary>
    public const string PasswordExpireDay = "PasswordExpireDay";
    
    /// <summary>
    /// 密码过期，提前通知天数
    /// </summary>
    public const string PasswordRemindDay = "PasswordRemindDay";
    
    /// <summary>
    /// 首次修改密码时间
    /// </summary>
    public const string FirstChangePasswordTime = "FirstChangePasswordTime";
    
    /// <summary>
    /// 最后修改密码时间
    /// </summary>
    public const string LastChangePasswordTime = "LastChangePasswordTime";
}

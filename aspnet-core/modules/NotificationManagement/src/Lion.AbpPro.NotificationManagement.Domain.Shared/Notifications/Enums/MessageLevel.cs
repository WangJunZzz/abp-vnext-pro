using System.ComponentModel;

namespace Lion.AbpPro.NotificationManagement.Notifications.Enums;

/// <summary>
/// 消息等级
/// </summary>
public enum MessageLevel
{
    /// <summary>
    /// 警告
    /// </summary>
    [Description("警告")]
    Warning = 10,
    /// <summary>
    /// 正常
    /// </summary>
    [Description("正常")]
    Information = 20,
    /// <summary>
    /// 错误
    /// </summary>
    [Description("错误")]
    Error = 30,
}
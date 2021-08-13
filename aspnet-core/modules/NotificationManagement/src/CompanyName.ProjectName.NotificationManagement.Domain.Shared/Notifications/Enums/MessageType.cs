using System.ComponentModel;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    /// <summary>
    /// 消息类型 
    /// </summary>
    public enum MessageType
    {

        /// <summary>
        /// 广播消息
        /// </summary>
        [Description("广播消息")]
        BroadCast = 10,
        /// <summary>
        /// 普通文本消息
        /// </summary>
        [Description("普通文本消息")]
        Text = 20,

    }
}
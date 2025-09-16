namespace Lion.AbpPro.SignalR.LocalEvent.Notification;

/// <summary>
/// 创建消息本地事件
/// </summary>
public class CreatedNotificationLocalEvent
{
    public CreatedNotificationLocalEvent(Guid id, Guid? tenantId, string title, string content, MessageType messageType, MessageLevel messageLevel, Guid senderUserId, string senderUserName, Guid? receiveUserId, string receiveUserName, bool isPersistent)
    {
        Id = id;
        TenantId = tenantId;
        Title = title;
        Content = content;
        MessageType = messageType;
        MessageLevel = messageLevel;
        SenderUserId = senderUserId;
        SenderUserName = senderUserName;
        ReceiveUserId = receiveUserId;
        ReceiveUserName = receiveUserName;
        IsPersistent = isPersistent;
    }

    /// <summary>
    /// 消息id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 租户id
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// 消息标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public MessageType MessageType { get; set; }

    /// <summary>
    /// 消息等级
    /// </summary>
    public MessageLevel MessageLevel { get; set; }

    /// <summary>
    /// 发送人
    /// </summary>
    public Guid SenderUserId { get; set; }

    /// <summary>
    /// 发送人用户名
    /// </summary>
    public string SenderUserName { get; set; }

    /// <summary>
    /// 订阅人
    /// 消息类型是广播消息时，订阅人为空
    /// </summary>
    public Guid? ReceiveUserId { get; set; }

    /// <summary>
    /// 接收人用户名
    /// 消息类型是广播消息时，订接收人用户名为空
    /// </summary>
    public string ReceiveUserName { get; set; }

    /// <summary>
    /// 消息是否持久化
    /// </summary>
    public bool IsPersistent { get; set; }
}
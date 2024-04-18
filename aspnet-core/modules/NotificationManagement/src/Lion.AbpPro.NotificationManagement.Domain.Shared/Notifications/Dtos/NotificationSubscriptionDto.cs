namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos;

public class NotificationSubscriptionDto
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// 租户id
    /// </summary>
    public Guid? TenantId { get; private set; }

    /// <summary>
    /// 消息Id
    /// </summary>
    public Guid NotificationId { get; private set; }

    /// <summary>
    /// 接收人id
    /// </summary>
    public Guid ReceiveUserId { get; private set; }

    /// <summary>
    /// 接收人用户名
    /// </summary>
    public string ReceiveUserName { get; private set; }

    /// <summary>
    /// 是否已读
    /// </summary>
    public bool Read { get; private set; }

    /// <summary>
    /// 已读时间
    /// </summary>
    public DateTime ReadTime { get; private set; }
}
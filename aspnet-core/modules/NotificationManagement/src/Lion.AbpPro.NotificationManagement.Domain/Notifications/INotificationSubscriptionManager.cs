using Lion.AbpPro.NotificationManagement.Notifications.Dtos;

namespace Lion.AbpPro.NotificationManagement.Notifications;

public interface INotificationSubscriptionManager
{
    /// <summary>
    /// 设置已读
    /// </summary>
    Task SetReadAsync(Guid receiveUserId, string receiveUserName, Guid notificationId);
    
    /// <summary>
    /// 分页获取消息
    /// </summary>
    Task<List<NotificationSubscriptionDto>> GetPagingListAsync(
        Guid notificationId,
        Guid? receiverUserId,
        string receiverUserName,
        DateTime? startReadTime,
        DateTime? endReadTime,
        int maxResultCount = 10,
        int skipCount = 0,
        CancellationToken cancellationToken = default);
        
    /// <summary>
    /// 获取消息总条数
    /// </summary>
    Task<long> GetPagingCountAsync(  
        Guid notificationId,
        Guid? receiverUserId,
        string receiverUserName,
        DateTime? startReadTime,
        DateTime? endReadTime,
        CancellationToken cancellationToken = default);

    Task<List<NotificationSubscriptionDto>> GetListAsync(List<Guid> notificationId, Guid receiverUserId, CancellationToken cancellationToken = default);

 
    Task<NotificationSubscriptionDto> FindAsync(Guid receiveUserId, Guid notificationId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// 删除消息
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
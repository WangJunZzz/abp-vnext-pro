using Lion.AbpPro.NotificationManagement.Notifications.Dtos;
using Lion.AbpPro.SignalR.Enums;

namespace Lion.AbpPro.NotificationManagement.Notifications;

public interface INotificationManager
{
    /// <summary>
    /// 分页获取消息
    /// </summary>
    Task<List<NotificationDto>> GetPagingListAsync(
        string title,
        string content,
        Guid? senderUserId,
        string senderUserName,
        Guid? receiverUserId,
        string receiverUserName,
        bool? read,
        DateTime? startReadTime,
        DateTime? endReadTime,
        MessageType? messageType,
        MessageLevel? messageLevel,
        int maxResultCount = 10,
        int skipCount = 0);

    /// <summary>
    /// 获取消息总条数
    /// </summary>
    Task<long> GetPagingCountAsync(
        string title,
        string content,
        Guid? senderUserId,
        string senderUserName,
        Guid? receiverUserId,
        string receiverUserName,
        bool? read,
        DateTime? startReadTime,
        DateTime? endReadTime,
        MessageType? messageType,
        MessageLevel? messageLevel);

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="title">标题</param>
    /// <param name="content">消息内容</param>
    /// <param name="messageType">消息类型</param>
    /// <param name="level">消息等级</param>
    /// <param name="receiveUserId">接受人，发送给谁。</param>
    /// <param name="receiveUserName">接受人用户名</param>
    Task CreateAsync(Guid id, string title, string content,MessageType messageType, MessageLevel level, Guid? receiveUserId,string receiveUserName);
    
    /// <summary>
    /// 消息设置为已读
    /// </summary>
    /// <param name="id">消息Id</param>
    Task SetReadAsync(Guid id);

    Task<NotificationDto> GetAsync(Guid id);
    
    Task<List<NotificationDto>> GetListAsync(List<Guid> ids);
    
    Task DeleteAsync(Guid id);
}
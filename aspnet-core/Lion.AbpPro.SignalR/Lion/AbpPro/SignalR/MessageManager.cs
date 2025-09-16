using Lion.AbpPro.SignalR.Hubs;
using Lion.AbpPro.SignalR.LocalEvent.Notification;

namespace Lion.AbpPro.SignalR;

public class MessageManager : IMessageManager, ITransientDependency
{
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
    private readonly ILogger<MessageManager> _logger;
    private readonly ILocalEventBus _localEventBus;
    private readonly ICurrentUser _currentUser;
    private readonly ICurrentTenant _currentTenant;
    private readonly IGuidGenerator _guidGenerator;

    public MessageManager(IHubContext<NotificationHub, INotificationHub> hubContext, ILogger<MessageManager> logger, ILocalEventBus localEventBus, ICurrentUser currentUser, ICurrentTenant currentTenant, IGuidGenerator guidGenerator)
    {
        _hubContext = hubContext;
        _logger = logger;
        _localEventBus = localEventBus;
        _currentUser = currentUser;
        _currentTenant = currentTenant;
        _guidGenerator = guidGenerator;
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="title">消息标题</param>
    /// <param name="content">消息内容</param>
    /// <param name="messageType">消息类型</param>
    /// <param name="messageLevel">消息级别</param>
    /// <param name="receiverUserId">消息接受人，如果是广播消息，不需要传递</param>
    /// <param name="receiverUserName">消息接受人userName，如果是广播消息，不需要传递</param>
    /// <param name="isPersistent">是否持久化,如果ture会在消息管理中出现,并且右上角也会存在</param>
    /// <returns></returns>
    public virtual async Task SendMessageAsync(string title, string content, MessageType messageType, MessageLevel messageLevel, Guid? receiverUserId = null, string receiverUserName = "", bool isPersistent = true)
    {
        var messageId = _guidGenerator.Create();
        if (messageType == MessageType.Common)
        {
            if (string.IsNullOrWhiteSpace(receiverUserId.ToString()))
            {
                _logger.LogError($"发送消息失败：接收用户ID为空,消息Id：{messageId}");
                return;
            }

            await _hubContext.Clients
                .Users([receiverUserId.ToString()])
                .ReceiveTextMessageAsync(new SendNotificationDto(messageId, title, content, messageType, messageLevel));
        }

        if (messageType == MessageType.BroadCast)
        {
            await _hubContext.Clients.All.ReceiveBroadCastMessageAsync(new SendNotificationDto(messageId, title, content, messageType, messageLevel));
        }

        if (isPersistent)
        {
            await _localEventBus.PublishAsync(new CreatedNotificationLocalEvent(messageId, _currentTenant.Id, title, content, messageType, messageLevel, _currentUser.GetId(), _currentUser.UserName, receiverUserId, receiverUserName, true));
        }
    }
}
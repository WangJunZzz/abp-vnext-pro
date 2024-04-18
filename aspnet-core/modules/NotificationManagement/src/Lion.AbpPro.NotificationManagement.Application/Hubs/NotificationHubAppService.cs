using Volo.Abp.Json;

namespace Lion.AbpPro.NotificationManagement.Hubs;

public class NotificationHubAppService : NotificationManagementAppService, INotificationHubAppService
{
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
    private readonly ILogger<NotificationAppService> _logger;
    private readonly IJsonSerializer _jsonSerializer;
    public NotificationHubAppService(
        IHubContext<NotificationHub, INotificationHub> hubContext,
        ILogger<NotificationAppService> logger, 
        IJsonSerializer jsonSerializer)
    {
        _hubContext = hubContext;
        _logger = logger;
        _jsonSerializer = jsonSerializer;
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public virtual async Task SendMessageAsync(Guid id, string title, string content, MessageType messageType, MessageLevel messageLevel, string receiverUserId)
    {
        switch (messageType)
        {
            case MessageType.Common:
                await SendMessageToClientByUserIdAsync(new SendNotificationDto(id, title, content, messageType, messageLevel), receiverUserId);
                break;
            case MessageType.BroadCast:
                await SendMessageToAllClientAsync(new SendNotificationDto(id, title, content, messageType, messageLevel));
                break;
            default:
                throw new BusinessException(NotificationManagementErrorCodes.MessageTypeUnknown);
        }
    }

    /// <summary>
    /// 发送消息指定客户端用户
    /// </summary>
    private async Task SendMessageToClientByUserIdAsync(SendNotificationDto sendNotificationDto, string receiverUserId)
    {
        if (receiverUserId.IsNotNullOrWhiteSpace())
        {
            await _hubContext.Clients
                .Users(new string[] { receiverUserId })
                .ReceiveTextMessageAsync(sendNotificationDto);
            _logger.LogInformation($"通知模块收到消息：{_jsonSerializer.Serialize(sendNotificationDto)},发送给：{receiverUserId}");
        }
        else
        {
            _logger.LogWarning($"消息未指定发送人:{_jsonSerializer.Serialize(sendNotificationDto)}");
        }
    }

    /// <summary>
    /// 发送消息到所有客户端
    /// 广播消息
    /// </summary>
    private async Task SendMessageToAllClientAsync(SendNotificationDto sendNotificationDto)
    {
        await _hubContext.Clients.All.ReceiveBroadCastMessageAsync(sendNotificationDto);
        _logger.LogInformation($"通知模块收到消息：{_jsonSerializer.Serialize(sendNotificationDto)}");
    }
}
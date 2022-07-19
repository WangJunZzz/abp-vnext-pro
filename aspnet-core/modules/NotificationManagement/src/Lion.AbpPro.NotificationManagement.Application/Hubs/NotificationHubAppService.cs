namespace Lion.AbpPro.NotificationManagement.Hubs;

public class NotificationHubAppService : NotificationManagementAppService, INotificationHubAppService
{
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
    private readonly ILogger<NotificationAppService> _logger;

    public NotificationHubAppService(
        IHubContext<NotificationHub, INotificationHub> hubContext,
        ILogger<NotificationAppService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public async Task SendMessageAsync(Guid id, string title, string content, MessageType messageType, MessageLevel messageLevel, List<string> users)
    {
        switch (messageType)
        {
            case MessageType.Common:
                await SendMessageToClientByUserIdAsync(new SendNotificationDto(id, title, content, messageType, messageLevel), users);
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
    private async Task SendMessageToClientByUserIdAsync(SendNotificationDto sendNotificationDto, List<string> users)
    {
        if (users is { Count: > 0 })
        {
            await _hubContext.Clients
                .Users(users.AsReadOnly().ToList())
                .ReceiveTextMessageAsync(sendNotificationDto);
            _logger.LogInformation($"通知模块收到消息：{JsonConvert.SerializeObject(sendNotificationDto)},发送给：{JsonConvert.SerializeObject(users)}");
        }
        else
        {
            _logger.LogWarning($"消息未指定发送人:{JsonConvert.SerializeObject(sendNotificationDto)}");
        }
    }

    /// <summary>
    /// 发送消息到所有客户端
    /// 广播消息
    /// </summary>
    private async Task SendMessageToAllClientAsync(SendNotificationDto sendNotificationDto)
    {
        await _hubContext.Clients.All.ReceiveBroadCastMessageAsync(sendNotificationDto);
        _logger.LogInformation($"通知模块收到消息：{JsonConvert.SerializeObject(sendNotificationDto)}");
    }
}
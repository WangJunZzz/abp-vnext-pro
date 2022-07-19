namespace Lion.AbpPro.NotificationManagement.Notifications.LocalEventHandlers
{
    /// <summary>
    /// 创建消息事件处理
    /// </summary>
    public class NotificationCreatedLocalEventHandler : 
            ILocalEventHandler<CreatedNotificationLocalEvent>,
            ITransientDependency
    {
        private readonly INotificationHubAppService _hubAppService;

        public NotificationCreatedLocalEventHandler(INotificationHubAppService hubAppService)
        {
            _hubAppService = hubAppService;
        }

        public virtual Task HandleEventAsync(CreatedNotificationLocalEvent eventData)
        {
            return _hubAppService.SendMessageAsync(
                eventData.NotificationEto.Id,
                eventData.NotificationEto.Title,
                eventData.NotificationEto.Content,
                eventData.NotificationEto.MessageType,
                eventData.NotificationEto.MessageLevel,
                eventData.NotificationEto.NotificationSubscriptions.Select(e => e.ReceiveId.ToString()).ToList());
        }
        
    }
}
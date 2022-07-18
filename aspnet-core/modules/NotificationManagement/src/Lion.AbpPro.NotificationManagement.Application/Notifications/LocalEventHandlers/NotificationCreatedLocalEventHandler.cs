using System.Linq;
using System.Threading.Tasks;
using Lion.AbpPro.NotificationManagement.Hubs;
using Lion.AbpPro.NotificationManagement.Notifications.DistributedEvents;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Lion.AbpPro.NotificationManagement.Notifications.LocalEventHandlers
{
    /// <summary>
    /// 创建消息事件处理
    /// </summary>
    public class NotificationCreatedLocalEventHandler : 
            ILocalEventHandler<CreatedNotificationDistributedEvent>,
            ITransientDependency
    {
        private readonly INotificationHubAppService _hubAppService;

        public NotificationCreatedLocalEventHandler(INotificationHubAppService hubAppService)
        {
            _hubAppService = hubAppService;
        }

        public virtual Task HandleEventAsync(CreatedNotificationDistributedEvent eventData)
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
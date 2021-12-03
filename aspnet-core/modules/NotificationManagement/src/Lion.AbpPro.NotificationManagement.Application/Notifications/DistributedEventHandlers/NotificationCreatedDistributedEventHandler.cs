using System.Linq;
using System.Threading.Tasks;
using Lion.AbpPro.NotificationManagement.Notifications.DistributedEvents;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Lion.AbpPro.NotificationManagement.Notifications.DistributedEventHandlers
{
    /// <summary>
    /// 创建消息事件处理
    /// </summary>
    public class NotificationCreatedDistributedEventHandler : 
            IDistributedEventHandler<CreatedNotificationDistributedEvent>,
            ITransientDependency
    {
        private readonly INotificationAppService _hubAppService;

        public NotificationCreatedDistributedEventHandler(INotificationAppService hubAppService)
        {
            _hubAppService = hubAppService;
        }

        public virtual Task HandleEventAsync(CreatedNotificationDistributedEvent eventData)
        {
            return _hubAppService.SendMessageAsync(
                eventData.NotificationEto.Title,
                eventData.NotificationEto.Content,
                eventData.NotificationEto.MessageType,
                eventData.NotificationEto.NotificationSubscriptions.Select(e => e.ReceiveId.ToString()).ToList());
        }
    }
}
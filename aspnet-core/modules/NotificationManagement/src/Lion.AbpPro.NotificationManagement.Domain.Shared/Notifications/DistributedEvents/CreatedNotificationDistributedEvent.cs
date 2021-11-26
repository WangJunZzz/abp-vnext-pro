using Lion.AbpPro.NotificationManagement.Notifications.Etos;

namespace Lion.AbpPro.NotificationManagement.Notifications.DistributedEvents
{
    public class CreatedNotificationDistributedEvent
    {
        public NotificationEto NotificationEto { get;  set; }

        private CreatedNotificationDistributedEvent()
        {
            
        }

        public CreatedNotificationDistributedEvent(NotificationEto notificationEto)
        {
            NotificationEto = notificationEto;
        }
    }
}
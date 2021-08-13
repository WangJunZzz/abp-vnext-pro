using CompanyName.ProjectName.NotificationManagement.Notifications.Etos;

namespace CompanyName.ProjectName.NotificationManagement.Notifications.DistributedEvents
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
using AutoMapper;
using Lion.AbpPro.NotificationManagement.Notifications.Etos;

namespace Lion.AbpPro.NotificationManagement.Notifications
{
    public class NotificationDomainAutoMapperProfile:Profile
    {
        public NotificationDomainAutoMapperProfile()
        {
            CreateMap<Notification, NotificationEto>();
            CreateMap<NotificationSubscription, NotificationSubscriptionEto>();
        }
    }
}
using AutoMapper;
using Lion.AbpPro.NotificationManagement.Notifications;
using Lion.AbpPro.NotificationManagement.Notifications.Etos;

namespace Lion.AbpPro.NotificationManagement
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
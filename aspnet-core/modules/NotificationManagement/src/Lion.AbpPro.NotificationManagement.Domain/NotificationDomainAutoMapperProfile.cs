using Lion.AbpPro.NotificationManagement.Notifications.Dtos;

namespace Lion.AbpPro.NotificationManagement
{
    public class NotificationDomainAutoMapperProfile:Profile
    {
        public NotificationDomainAutoMapperProfile()
        {
            CreateMap<Notification, NotificationEto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationSubscription, NotificationSubscriptionDto>();
        }
    }
}
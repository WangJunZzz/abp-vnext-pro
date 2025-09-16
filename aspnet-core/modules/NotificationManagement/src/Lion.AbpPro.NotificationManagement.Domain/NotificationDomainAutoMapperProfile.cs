using Lion.AbpPro.NotificationManagement.Notifications.Dtos;

namespace Lion.AbpPro.NotificationManagement
{
    public class NotificationDomainAutoMapperProfile:Profile
    {
        public NotificationDomainAutoMapperProfile()
        {
            CreateMap<NotificationSubscription, NotificationSubscriptionDto>();
        }
    }
}
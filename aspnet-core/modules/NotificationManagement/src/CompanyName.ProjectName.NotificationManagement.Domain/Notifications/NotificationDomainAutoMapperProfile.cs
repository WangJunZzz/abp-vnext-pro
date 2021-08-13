using AutoMapper;
using CompanyName.ProjectName.NotificationManagement.Notifications.Etos;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
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
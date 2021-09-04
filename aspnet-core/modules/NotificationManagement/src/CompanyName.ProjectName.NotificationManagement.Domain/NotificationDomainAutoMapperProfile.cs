using AutoMapper;
using CompanyName.ProjectName.NotificationManagement.Notifications;
using CompanyName.ProjectName.NotificationManagement.Notifications.Etos;

namespace CompanyName.ProjectName.NotificationManagement
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
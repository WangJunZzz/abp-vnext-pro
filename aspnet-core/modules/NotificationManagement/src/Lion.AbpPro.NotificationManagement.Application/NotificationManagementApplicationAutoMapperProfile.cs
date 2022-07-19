namespace Lion.AbpPro.NotificationManagement
{
    public class NotificationManagementApplicationAutoMapperProfile : Profile
    {
        public NotificationManagementApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Notification, PagingNotificationListOutput>()
                .ForMember(dest => dest.Read, opt => opt.MapFrom(e => e.NotificationSubscriptions.FirstOrDefault().Read));
        }
    }
}
namespace Lion.AbpPro.NotificationManagement
{
    public class NotificationManagementApplicationAutoMapperProfile : Profile
    {
        public NotificationManagementApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<NotificationDto, PagingNotificationOutput>();
            CreateMap<NotificationSubscriptionDto, PagingNotificationSubscriptionOutput>()
                .ForMember(dest => dest.Title, opt => opt.Ignore())
                .ForMember(dest => dest.Content, opt => opt.Ignore())
                .ForMember(dest => dest.MessageType, opt => opt.Ignore())
                .ForMember(dest => dest.MessageLevel, opt => opt.Ignore())
                .ForMember(dest => dest.SenderUserId, opt => opt.Ignore())
                .ForMember(dest => dest.SenderUserName, opt => opt.Ignore());
        }
    }
}
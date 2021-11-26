using System;

namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos
{
    public class PagingNotificationListOutput
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public bool Read { get; set; }
    }
}
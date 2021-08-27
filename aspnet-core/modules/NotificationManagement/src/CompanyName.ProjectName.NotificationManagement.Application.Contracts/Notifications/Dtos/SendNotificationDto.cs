namespace CompanyName.ProjectName.NotificationManagement.Notifications.Dtos
{
    public class SendNotificationDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public MessageType MessageType { get; set; }

        private SendNotificationDto()
        {
            
        }

        public SendNotificationDto(string title, string content, MessageType messageType)
        {
            Title = title;
            Content = content;
            MessageType = messageType;
        }
    }
}
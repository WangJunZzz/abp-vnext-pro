namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos
{
    public class SendNotificationDto
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// 消息等级
        /// </summary>
        public MessageLevel MessageLevel { get;  set; }
        
        private SendNotificationDto()
        {
            
        }

        public SendNotificationDto(Guid id, string title, string content, MessageType messageType,MessageLevel messageLevel)
        {
            Id = id;
            Title = title;
            Content = content;
            MessageType = messageType;
            MessageLevel = messageLevel;
        }
    }
}
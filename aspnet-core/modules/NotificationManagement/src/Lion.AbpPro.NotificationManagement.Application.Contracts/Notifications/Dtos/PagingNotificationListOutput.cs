namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos
{
    public class PagingNotificationListOutput
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get;  set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get;  set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get;  set; }

        public string MessageTypeDescription => MessageType.ToDescription();
        
        /// <summary>
        /// 消息等级
        /// </summary>
        public MessageLevel MessageLevel { get;  set; }

        public string MessageLevelDescription => MessageLevel.ToDescription();

        /// <summary>
        /// 发送人
        /// </summary>
        public Guid SenderId { get;  set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get; set; }
    }
}
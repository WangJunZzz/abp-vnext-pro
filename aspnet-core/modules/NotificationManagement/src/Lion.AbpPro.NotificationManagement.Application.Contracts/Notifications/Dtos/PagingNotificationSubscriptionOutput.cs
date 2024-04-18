namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos
{
    public class PagingNotificationSubscriptionOutput
    {
        public Guid Id { get; set; }
    
        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId { get;  set; }

        /// <summary>
        /// 消息Id
        /// </summary>
        public Guid NotificationId { get;  set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        public Guid ReceiveUserId { get;  set; }

        /// <summary>
        /// 接收人用户名
        /// </summary>
        public string ReceiveUserName { get;  set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get;  set; }

        /// <summary>
        /// 已读时间
        /// </summary>
        public DateTime ReadTime { get;  set; }

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

        public string MessageTypeName => MessageType.ToDescription();

        /// <summary>
        /// 消息等级
        /// </summary>
        public MessageLevel MessageLevel { get; set; }
        public string MessageLevelName => MessageLevel.ToDescription();
        
        /// <summary>
        /// 发送人
        /// </summary>
        public Guid SenderUserId { get; set; }

        /// <summary>
        /// 发送人用户名
        /// </summary>
        public string SenderUserName { get; set; }
    }
}
using Volo.Abp.MultiTenancy;

namespace Lion.AbpPro.NotificationManagement.Notifications.Aggregates
{
    /// <summary>
    /// 消息通知 
    /// </summary>
    public class Notification : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId { get; private set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; private set; }

        /// <summary>
        /// 消息等级
        /// </summary>
        public MessageLevel MessageLevel { get; private set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public Guid SenderUserId { get; private set; }

        /// <summary>
        /// 发送人用户名
        /// </summary>
        public string SenderUserName { get; private set; }

        /// <summary>
        /// 订阅人
        /// 消息类型是广播消息时，订阅人为空
        /// </summary>
        public Guid? ReceiveUserId { get; private set; }


        /// <summary>
        /// 接收人用户名
        /// 消息类型是广播消息时，订接收人用户名为空
        /// </summary>
        public string ReceiveUserName { get; private set; }
        
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get; private set; }
        
        /// <summary>
        /// 已读时间
        /// </summary>
        public DateTime? ReadTime { get; private set; }

        
        private Notification()
        {
            
        }

        public Notification(
            Guid id,
            string title,
            string content,
            MessageType messageType,
            MessageLevel messageLevel,
            Guid senderUserId,
            string senderUserName,
            Guid? receiveUserId = null,
            string receiveUserName ="",
            DateTime? readTime = null,
            bool read = false,
            Guid? tenantId = null
        ) : base(id)
        {
            SetTitle(title);
            SetContent(content);
            SetMessageType(messageType);
            SetMessageLevel(messageLevel);
            SetSenderUserId(senderUserId);
            SetSenderUserName(senderUserName);
            SetReceiveUserId(receiveUserId);
            SetReceiveUserName(receiveUserName);
            SetTenantId(tenantId);
            SetRead(read,readTime);
        }
        

        private void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        private void SetSenderUserId(Guid senderUserId)
        {
            Guard.NotEmpty(senderUserId, nameof(senderUserId));
            SenderUserId = senderUserId;
        }
        
        private void SetSenderUserName(string senderUserName)
        {
            Guard.NotNullOrWhiteSpace(senderUserName, nameof(senderUserName), NotificationMaxLengths.Length128);
            SenderUserName = senderUserName;
        }
        
        private void SetReceiveUserId(Guid? receiveUserId)
        {
            ReceiveUserId = receiveUserId;
        }
        
        private void SetReceiveUserName(string receiveUserName)
        {
            ReceiveUserName = receiveUserName;
        }

        
        private void SetTitle(string title)
        {
            Guard.NotNullOrWhiteSpace(title, nameof(title), NotificationMaxLengths.Length128);
            Title = title;
        }

        private void SetContent(string content)
        {
            Guard.NotNullOrWhiteSpace(content, nameof(content), NotificationMaxLengths.Length1024);
            Content = content;
        }

        private void SetMessageType(MessageType messageType)
        {
            MessageType = messageType;
        }

        private void SetMessageLevel(MessageLevel messageLevel)
        {
            MessageLevel = messageLevel;
        }
        

        public void SetRead(bool read, DateTime? readTime = null)
        {
            Read = read;
            ReadTime = readTime;
        }

        /// <summary>
        /// 添加创建消息事件
        /// </summary>
        public void AddCreatedNotificationLocalEvent(CreatedNotificationLocalEvent createdNotificationLocalEvent)
        {
            AddLocalEvent(createdNotificationLocalEvent);
        }
    }
}
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
        [StringLength(NotificationMaxLengths.Title)]
        [Required]
        public string Title { get; private set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [StringLength(NotificationMaxLengths.Content)]
        [Required]
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
        public Guid SenderId { get; private set; }

        /// <summary>
        /// 关联属性1:N 消息订阅者集合
        /// </summary>
        public List<NotificationSubscription> NotificationSubscriptions { get; private set; }

        private Notification()
        {
            NotificationSubscriptions = new List<NotificationSubscription>();
        }

        public Notification(
            Guid id,
            string title,
            string content,
            MessageType messageType,
            MessageLevel messageLevel,
            Guid senderId,
            Guid? tenantId = null
        ) : base(id)
        {
            NotificationSubscriptions = new List<NotificationSubscription>();

            SetProperties(
                title,
                content,
                messageType,
                messageLevel,
                senderId
            );
            SetTenantId(tenantId);
        }

        private void SetProperties(
            string title,
            string content,
            MessageType messageType,
            MessageLevel messageLevel,
            Guid senderId
        )
        {
            SetTitle(title);
            SetContent(content);
            SetMessageType(messageType);
            SetMessageLevel(messageLevel);
            SetSenderId(senderId);
        }

        private void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }
        
        private void SetSenderId(Guid senderId)
        {
            Guard.NotEmpty(senderId, nameof(senderId));
            SenderId = senderId;
        }

        private void SetTitle(string title)
        {
            Guard.NotNullOrWhiteSpace(title, nameof(title), NotificationMaxLengths.Title);
            Title = title;
        }

        private void SetContent(string content)
        {
            Guard.NotNullOrWhiteSpace(content, nameof(content), NotificationMaxLengths.Content);
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

        /// <summary>
        /// 新增非广播消息订阅人
        /// </summary>
        public void AddNotificationSubscription(Guid notificationSubscriptionId, Guid receiveId)
        {
            if (NotificationSubscriptions.Any(e => e.ReceiveId == receiveId)) return;
            NotificationSubscriptions.Add(
                new NotificationSubscription(notificationSubscriptionId, Id, receiveId));
        }

        /// <summary>
        /// 新增消息类型为广播订阅人
        /// </summary>
        public void AddBroadCastNotificationSubscription(Guid notificationSubscriptionId, Guid receiveId, DateTime readTime)
        {
            if (NotificationSubscriptions.Any(e => e.ReceiveId == receiveId))
            {
                return;
            }

            var temp = new NotificationSubscription(notificationSubscriptionId, Id, receiveId);
            temp.SetRead(readTime);
            NotificationSubscriptions.Add(temp);
        }

        /// <summary>
        /// 添加创建消息事件
        /// </summary>
        public void AddCreatedNotificationLocalEvent(
            CreatedNotificationLocalEvent createdNotificationLocalEvent)
        {
            AddLocalEvent(createdNotificationLocalEvent);
        }
    }
}
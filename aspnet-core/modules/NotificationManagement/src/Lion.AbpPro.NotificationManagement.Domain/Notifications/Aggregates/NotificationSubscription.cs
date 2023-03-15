namespace Lion.AbpPro.NotificationManagement.Notifications.Aggregates
{
    /// <summary>
    /// 消息订阅者 
    /// </summary>
    public  class NotificationSubscription : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public Guid NotificationId { get; set; }
        
        /// <summary>
        /// 订阅人
        /// </summary>
        public Guid ReceiveId { get; private set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get; private set; }

        /// <summary>
        /// 已读时间
        /// </summary>
        public DateTime? ReadTime { get; private set; }


        private NotificationSubscription()
        {
        }

        public NotificationSubscription(
            Guid id,
            Guid notificationId,
            Guid receiveId
        ) : base(id)
        {
            SetNotificationId(notificationId);
            SetReceiveId(receiveId);
            Read = false;
            ReadTime = null;
        }

        private void SetNotificationId(Guid notificationId)
        {
            NotificationId = notificationId;
        }
        private void SetReceiveId(Guid receiveId)
        {
            ReceiveId = receiveId;
        }

        public void SetRead(DateTime readTime)
        {
            Read = true;
            ReadTime = readTime;
        }
    }
}
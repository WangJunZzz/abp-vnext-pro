using Volo.Abp.MultiTenancy;

namespace Lion.AbpPro.NotificationManagement.Notifications.Aggregates
{
    /// <summary>
    /// 消息订阅者 
    /// </summary>
    public class NotificationSubscription : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId { get; private set; }

        /// <summary>
        /// 消息Id
        /// </summary>
        public Guid NotificationId { get; private set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        public Guid ReceiveUserId { get; private set; }

        /// <summary>
        /// 接收人用户名
        /// </summary>
        public string ReceiveUserName { get; private set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get; private set; }

        /// <summary>
        /// 已读时间
        /// </summary>
        public DateTime ReadTime { get; private set; }


        private NotificationSubscription()
        {
        }

        public NotificationSubscription(
            Guid id,
            Guid notificationId,
            Guid receiveUserId,
            string receiveUserName,
            DateTime readTime,
            bool read = true,
            Guid? tenantId = null
        ) : base(id)
        {
            SetNotificationId(notificationId);
            SetReceiveUserId(receiveUserId);
            SetReceiveUserName(receiveUserName);
            SetRead(read, readTime);
            SetTenantId(tenantId);
        }

        public void SetRead(bool read, DateTime readTime)
        {
            Read = read;
            ReadTime = readTime;
        }

        private void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        private void SetNotificationId(Guid notificationId)
        {
            NotificationId = notificationId;
        }

        private void SetReceiveUserId(Guid receiveUserId)
        {
            ReceiveUserId = receiveUserId;
        }

        private void SetReceiveUserName(string receiveUserName)
        {
            Guard.NotNullOrWhiteSpace(receiveUserName, nameof(receiveUserName), NotificationMaxLengths.Length128);
            ReceiveUserName = receiveUserName;
        }


        public void SetRead(DateTime readTime)
        {
            Read = true;
            ReadTime = readTime;
        }
    }
}
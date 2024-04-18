namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos
{
    public class PagingNotificationSubscriptionInput : PagingBase
    {

        public Guid NotificationId { get; set; }
       
 
        /// <summary>
        /// 接受者Id
        /// </summary>
        public Guid? ReceiverUserId { get; set; }

        /// <summary>
        /// 接受者名称
        /// </summary>
        public string ReceiverUserName { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool? Read { get; set; }

        /// <summary>
        /// 已读开始时间
        /// </summary>
        public DateTime? StartReadTime { get; set; }

        /// <summary>
        /// 已读结束时间
        /// </summary>
        public DateTime? EndReadTime { get; set; }
    }
}
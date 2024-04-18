namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos
{
    public class PagingNotificationInput : PagingBase
    {
        /// <summary>
        ///  标题
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        ///  内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送者Id
        /// </summary>
        public Guid? SenderUserId { get; set; }

        /// <summary>
        /// 发送者名称
        /// </summary>
        public string SenderUserName { get; set; }

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

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType? MessageType { get; set; }
    }
}
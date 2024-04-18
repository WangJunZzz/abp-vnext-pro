using Lion.AbpPro.NotificationManagement.Notifications.Enums;

namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId { get; set; }

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
        public MessageLevel MessageLevel { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public Guid SenderUserId { get; set; }

        /// <summary>
        /// 发送人用户名
        /// </summary>
        public string SenderUserName { get; set; }

        /// <summary>
        /// 订阅人
        /// 消息类型是广播消息时，订阅人为空
        /// </summary>
        public Guid? ReceiveUserId { get; set; }


        /// <summary>
        /// 接收人用户名
        /// 消息类型是广播消息时，订接收人用户名为空
        /// </summary>
        public string ReceiveUserName { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get; set; }

        /// <summary>
        /// 已读时间
        /// </summary>
        public DateTime? ReadTime { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
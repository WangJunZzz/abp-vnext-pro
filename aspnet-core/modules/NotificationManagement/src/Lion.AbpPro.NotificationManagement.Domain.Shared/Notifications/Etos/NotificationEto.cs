using System;
using System.Collections.Generic;
using Lion.AbpPro.NotificationManagement.Notifications.Enums;


namespace Lion.AbpPro.NotificationManagement.Notifications.Etos
{
    public class NotificationEto
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
        
        /// <summary>
        /// 发送人
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// 关联属性1:N 消息订阅者集合
        /// </summary>
        public List<NotificationSubscriptionEto> NotificationSubscriptions { get;  set; }
    }

    public class NotificationSubscriptionEto
    {
        /// <summary>
        /// 订阅人
        /// </summary>
        public Guid ReceiveId { get;  set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get; set; }

        /// <summary>
        /// 已读时间
        /// </summary>
        public DateTime? ReadTime { get; set; }
    }
}
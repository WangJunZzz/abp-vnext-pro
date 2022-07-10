using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Lion.AbpPro.Extension.Customs;
using Lion.AbpPro.NotificationManagement.Notifications.DistributedEvents;
using Lion.AbpPro.NotificationManagement.Notifications.Enums;
using Lion.AbpPro.NotificationManagement.Notifications.MaxLengths;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lion.AbpPro.NotificationManagement.Notifications.Aggregates
{
    /// <summary>
    /// 消息通知 
    /// </summary>
    public  class Notification : FullAuditedAggregateRoot<Guid>
    {
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
            Guid senderId
        ) : base(id)
        {
            NotificationSubscriptions = new List<NotificationSubscription>();

            SetProperties(
                title,
                content,
                messageType,
                senderId
            );
        }

        private void SetProperties(
            string title,
            string content,
            MessageType messageType,
            Guid senderId
        )
        {
            SetTitle(title);
            SetContent(content);
            SetMessageType(messageType);
            SetSenderId(senderId);
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

        /// <summary>
        /// 新增非广播消息订阅人
        /// </summary>
        /// <param name="notificationSubscriptionId"></param>
        /// <param name="receiveId"></param>
        public void AddNotificationSubscription(Guid notificationSubscriptionId, Guid receiveId)
        {
            if (NotificationSubscriptions.Any(e => e.ReceiveId == receiveId)) return;
            NotificationSubscriptions.Add(
                new NotificationSubscription(notificationSubscriptionId, receiveId));
        }

        /// <summary>
        /// 新增消息类型为广播订阅人
        /// </summary>
        /// <param name="notificationSubscriptionId"></param>
        /// <param name="receiveId"></param>
        public void AddBroadCastNotificationSubscription(Guid notificationSubscriptionId,
            Guid receiveId)
        {
            if (NotificationSubscriptions.Any(e => e.ReceiveId == receiveId))
            {
                return;
            }
            else
            {
                var temp = new NotificationSubscription(notificationSubscriptionId, receiveId);
                temp.SetRead();
                NotificationSubscriptions.Add(temp);
            }
        }

        /// <summary>
        /// 添加创建消息集成事件
        /// </summary>
        /// <param name="createdNotificationDistributedEvent"></param>
        public void AddCreatedNotificationDistributedEvent(
            CreatedNotificationDistributedEvent createdNotificationDistributedEvent)
        {
            AddDistributedEvent(createdNotificationDistributedEvent);
        }
    }
}
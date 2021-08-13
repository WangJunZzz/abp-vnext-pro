using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.NotificationManagement.Notifications.DistributedEvents;
using CompanyName.ProjectName.NotificationManagement.Notifications.Etos;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    public class NotificationManager : NotificationManagementDomainService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationManager(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
            
        }


        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="senderId">发送人id,可以直接取ICurrentUser对象的用户</param>
        /// <param name="receiveIds">消息接收人</param>
        /// <param name="messageType">消息类似 10 广播消息，所有用户都可以接收到 ；20 普通文本消息 需要指定接收用户</param>
        public async Task CreateAsync(
            string title,
            string content,
            Guid senderId,
            List<Guid> receiveIds,
            MessageType messageType = MessageType.Text)
        {
            var entity = new Notification(GuidGenerator.Create(), title, content, messageType, senderId);

            if (messageType == MessageType.Text)
            {
                if (receiveIds is {Count: > 0})
                {
                    receiveIds.ForEach(item => { entity.AddNotificationSubscription(GuidGenerator.Create(), item); });
                } 
            }

            entity = await _notificationRepository.InsertAsync(entity);
            var notificationEto = ObjectMapper.Map<Notification, NotificationEto>(entity);
            // 发送集成事件
            entity.AddCreatedNotificationDistributedEvent(new CreatedNotificationDistributedEvent(notificationEto));
        }

        /// <summary>
        /// 消息设置为已读
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receiveId"></param>
        /// <exception cref="NotificationManagementDomainException"></exception>
        public async Task SetReadAsync(Guid id, Guid receiveId)
        {
            var notification = await _notificationRepository.FindByIdAsync(id);
            if (notification.MessageType == MessageType.BroadCast)
            {
                // 如果类型是广播消息，用户设置为已读，在插入一条数据
                notification.AddBroadCastNotificationSubscription(GuidGenerator.Create(), receiveId);
            }
            else
            {
                if (notification == null) throw new NotificationManagementDomainException(message: "消息不存在");
                var notificationSubscription =
                    notification.NotificationSubscriptions.FirstOrDefault(e => e.ReceiveId == receiveId);
                if (notificationSubscription == null)
                    throw new NotificationManagementDomainException(message: "当前用户未订阅该消息");
                notificationSubscription.SetRead(); 
            }

            await _notificationRepository.UpdateAsync(notification);
        }
    }
}
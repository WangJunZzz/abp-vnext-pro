using System.Security.Authentication;
using Lion.AbpPro.SignalR;
using Lion.AbpPro.SignalR.Enums;

namespace Lion.AbpPro.NotificationManagement.Notifications
{
    [Authorize]
    public class NotificationAppService : NotificationManagementAppService, INotificationAppService
    {
        private readonly INotificationManager _notificationManager;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IMessageManager _messageManager;


        public NotificationAppService(INotificationManager notificationManager, INotificationSubscriptionManager notificationSubscriptionManager, IMessageManager messageManager)
        {
            _notificationManager = notificationManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _messageManager = messageManager;
        }


        /// <summary>
        /// 发送警告文本消息
        /// </summary>
        public virtual async Task SendCommonWarningMessageAsync(SendCommonMessageInput input)
        {
            await _messageManager.SendMessageAsync(input.Title, input.Content, MessageType.Common, MessageLevel.Warning, CurrentUser.GetId(), CurrentUser.UserName, input.ReceiveUserId, input.ReceiveUserName, CurrentTenant.Id);
        }

        /// <summary>
        /// 发送普通文本消息
        /// </summary>
        public virtual async Task SendCommonInformationMessageAsync(SendCommonMessageInput input)
        {
            await _messageManager.SendMessageAsync(input.Title, input.Content, MessageType.Common, MessageLevel.Information, CurrentUser.GetId(), CurrentUser.UserName, input.ReceiveUserId, input.ReceiveUserName, CurrentTenant.Id);
        }

        /// <summary>
        /// 发送错误文本消息
        /// </summary>
        public virtual async Task SendCommonErrorMessageAsync(SendCommonMessageInput input)
        {
            await _messageManager.SendMessageAsync(input.Title, input.Content, MessageType.Common, MessageLevel.Error, CurrentUser.GetId(), CurrentUser.UserName, input.ReceiveUserId, input.ReceiveUserName, CurrentTenant.Id);
        }

        /// <summary>
        /// 发送警告广播消息
        /// </summary>
        public virtual async Task SendBroadCastWarningMessageAsync(SendBroadCastMessageInput input)
        {
            await _messageManager.SendMessageAsync(input.Title, input.Content, MessageType.BroadCast, MessageLevel.Warning, CurrentUser.GetId(), CurrentUser.UserName, tenantId: CurrentTenant.Id);
        }

        /// <summary>
        /// 发送正常广播消息
        /// </summary>
        public virtual async Task SendBroadCastInformationMessageAsync(SendBroadCastMessageInput input)
        {
            await _messageManager.SendMessageAsync(input.Title, input.Content, MessageType.BroadCast, MessageLevel.Information, CurrentUser.GetId(), CurrentUser.UserName, tenantId: CurrentTenant.Id);
        }

        /// <summary>
        /// 发送错误广播消息
        /// </summary>
        public virtual async Task SendBroadCastErrorMessageAsync(SendBroadCastMessageInput input)
        {
            await _messageManager.SendMessageAsync(input.Title, input.Content, MessageType.BroadCast, MessageLevel.Error, CurrentUser.GetId(), CurrentUser.UserName, tenantId: CurrentTenant.Id);
        }

        public virtual async Task SetReadAsync(SetReadInput input)
        {
            var notification = await _notificationManager.GetAsync(input.Id);

            if (notification == null)
            {
                throw new UserFriendlyException("消息不存在");
            }

            if (notification.MessageType == MessageType.Common)
            {
                await _notificationManager.SetReadAsync(input.Id);
            }
            else
            {
                if (!CurrentUser.IsAuthenticated)
                {
                    throw new AuthenticationException();
                }

                await _notificationSubscriptionManager.SetReadAsync(CurrentUser.GetId(), CurrentUser.UserName, input.Id);
            }
        }

        public virtual async Task SetBatchReadAsync(SetBatchReadInput input)
        {
            foreach (var item in input.Ids)
            {
                await SetReadAsync(new SetReadInput() { Id = item });
            }
        }

        /// <summary>
        /// 分页获取消息
        /// </summary>
        public virtual async Task<PagedResultDto<PagingNotificationOutput>> PageNotificationAsync(PagingNotificationInput input)
        {
            var totalCount = await _notificationManager.GetPagingCountAsync(input.Title, input.Content, input.SenderUserId, input.SenderUserName, input.ReceiverUserId, input.ReceiverUserName, input.Read, input.StartReadTime, input.EndReadTime,
                input.MessageType, input.MessageLevel);
            var list = await _notificationManager.GetPagingListAsync(input.Title, input.Content, input.SenderUserId, input.SenderUserName, input.ReceiverUserId, input.ReceiverUserName, input.Read, input.StartReadTime, input.EndReadTime,
                input.MessageType, input.MessageLevel, input.PageSize, input.SkipCount);
            // var boardCastNotificationIds = list.Where(e => e.MessageType == MessageType.BroadCast).Select(e => e.Id).ToList();
            // // 获取通告消息当前用户是否已读
            // var boardCastNotificationSubscriptions = await _notificationSubscriptionManager.GetListAsync(boardCastNotificationIds, CurrentUser.GetId());
            // foreach (var item in list)
            // {
            //     var sub = boardCastNotificationSubscriptions.FirstOrDefault(e => e.NotificationId == item.Id);
            //     item.Read = sub != null;
            //     item.ReceiveUserId = sub?.ReceiveUserId;
            //     item.ReceiveUserName = sub?.ReceiveUserName;
            // }
            return new PagedResultDto<PagingNotificationOutput>(totalCount, ObjectMapper.Map<List<NotificationDto>, List<PagingNotificationOutput>>(list));
        }

        /// <summary>
        /// 分页获取消息
        /// </summary>
        public virtual async Task<PagedResultDto<PagingNotificationSubscriptionOutput>> PageNotificationSubscriptionAsync(PagingNotificationSubscriptionInput input)
        {
            var totalCount = await _notificationSubscriptionManager.GetPagingCountAsync(input.NotificationId, input.ReceiverUserId, input.ReceiverUserName, input.StartReadTime, input.EndReadTime);
            var list = await _notificationSubscriptionManager.GetPagingListAsync(input.NotificationId, input.ReceiverUserId, input.ReceiverUserName, input.StartReadTime, input.EndReadTime, input.PageSize, input.SkipCount);
            var result = new PagedResultDto<PagingNotificationSubscriptionOutput>(totalCount, ObjectMapper.Map<List<NotificationSubscriptionDto>, List<PagingNotificationSubscriptionOutput>>(list));
            // 获取消息内容
            if (totalCount > 0)
            {
                var notifications = await _notificationManager.GetListAsync(list.Select(e => e.NotificationId).Distinct().ToList());
                foreach (var item in result.Items)
                {
                    var notification = notifications.FirstOrDefault(e => e.Id == item.NotificationId);
                    if (notification != null)
                    {
                        item.Title = notification.Title;
                        item.Content = notification.Content;
                        item.MessageType = notification.MessageType;
                        item.MessageLevel = notification.MessageLevel;
                        item.SenderUserId = notification.SenderUserId;
                        item.SenderUserName = notification.SenderUserName;
                    }
                }
            }

            return result;
        }

        public async Task DeleteAsync(DeleteMessageInput input)
        {
            var notification = await _notificationManager.GetAsync(input.Id);
            // 判断消息类型
            if (notification.MessageType == MessageType.Common)
            {
                await _notificationManager.DeleteAsync(notification.Id);
                return;
            }
            // todo 暂时只删除普通文本消息
            // if (notification.MessageType == MessageType.BroadCast && input.ReceiverUserId.HasValue)
            // {
            //     var subscription = await _notificationSubscriptionManager.FindAsync(input.ReceiverUserId.Value, input.Id);
            //     if (subscription != null)
            //     {
            //         await _notificationSubscriptionManager.DeleteAsync(subscription.Id);
            //     }
            // }
        }
    }
}
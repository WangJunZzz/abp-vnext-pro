using Lion.AbpPro.NotificationManagement.Notifications.LocalEvents;

namespace Lion.AbpPro.NotificationManagement.Notifications
{
    public class NotificationManager : NotificationManagementDomainService
    {
        private readonly INotificationRepository _notificationRepository;

        private readonly ICurrentUser _currentUser;

        public NotificationManager(INotificationRepository notificationRepository, ICurrentUser currentUser)
        {
            _notificationRepository = notificationRepository;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 分页获取消息
        /// </summary>
        public async Task<List<Notification>> GetPagingListAsync(
            Guid? userId,
            MessageType messageType,
            int maxResultCount = 10,
            int skipCount = 0)
        {
            return await _notificationRepository.GetPagingListAsync(userId, messageType, maxResultCount, skipCount);
        }

        /// <summary>
        /// 获取消息总条数
        /// </summary>
        public async Task<long> GetPagingCountAsync(Guid? userId, MessageType messageType)
        {
            return await _notificationRepository.GetPagingCountAsync(userId, messageType);
        }

        /// <summary>
        /// 发送警告文本消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="receiveIds">接受人，发送给谁。</param>
        public async Task SendCommonWarningMessageAsync(string title, string content, List<Guid> receiveIds)
        {
            if (receiveIds is { Count: 0 })
            {
                throw new NotificationManagementDomainException(NotificationManagementErrorCodes.ReceiverNotNull);
            }

            var senderId = Guid.Empty;
            if (_currentUser?.Id != null)
            {
                senderId = _currentUser.Id.Value;
            }

            var entity = new Notification(GuidGenerator.Create(), title, content, MessageType.Common, MessageLevel.Warning, senderId);
            foreach (var item in receiveIds)
            {
                entity.AddNotificationSubscription(GuidGenerator.Create(), item);
            }

            var notificationEto = ObjectMapper.Map<Notification, NotificationEto>(entity);
            // 发送集成事件
            entity.AddCreatedNotificationLocalEvent(new CreatedNotificationLocalEvent(notificationEto));
            await _notificationRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 发送普通文本消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="receiveIds">接受人，发送给谁。</param>
        public async Task SendCommonInformationMessageAsync(string title, string content, List<Guid> receiveIds)
        {
            if (receiveIds is { Count: 0 })
            {
                throw new NotificationManagementDomainException(NotificationManagementErrorCodes.ReceiverNotNull);
            }

            var senderId = Guid.Empty;
            if (_currentUser?.Id != null)
            {
                senderId = _currentUser.Id.Value;
            }

            var entity = new Notification(GuidGenerator.Create(), title, content, MessageType.Common, MessageLevel.Information, senderId);
            foreach (var item in receiveIds)
            {
                entity.AddNotificationSubscription(GuidGenerator.Create(), item);
            }

            var notificationEto = ObjectMapper.Map<Notification, NotificationEto>(entity);
            // 发送集成事件
            entity.AddCreatedNotificationLocalEvent(new CreatedNotificationLocalEvent(notificationEto));
            await _notificationRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 发送错误文本消息
        /// </summary>
        public async Task SendCommonErrorMessageAsync(string title, string content, List<Guid> receiveIds)
        {
            if (receiveIds is { Count: 0 })
            {
                throw new NotificationManagementDomainException(NotificationManagementErrorCodes.ReceiverNotNull);
            }

            var senderId = Guid.Empty;
            if (_currentUser?.Id != null)
            {
                senderId = _currentUser.Id.Value;
            }

            var entity = new Notification(GuidGenerator.Create(), title, content, MessageType.Common, MessageLevel.Error, senderId);
            foreach (var item in receiveIds)
            {
                entity.AddNotificationSubscription(GuidGenerator.Create(), item);
            }

            var notificationEto = ObjectMapper.Map<Notification, NotificationEto>(entity);
            // 发送集成事件
            entity.AddCreatedNotificationLocalEvent(new CreatedNotificationLocalEvent(notificationEto));
            await _notificationRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 发送警告广播消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">消息内容</param>
        public async Task SendBroadCastWarningMessageAsync(string title, string content)
        {
            var senderId = Guid.Empty;
            if (_currentUser?.Id != null)
            {
                senderId = _currentUser.Id.Value;
            }

            var entity = new Notification(GuidGenerator.Create(), title, content, MessageType.BroadCast, MessageLevel.Warning, senderId);
            var notificationEto = ObjectMapper.Map<Notification, NotificationEto>(entity);
            // 发送集成事件
            entity.AddCreatedNotificationLocalEvent(new CreatedNotificationLocalEvent(notificationEto));
            await _notificationRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 发送正常广播消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">消息内容</param>
        public async Task SendBroadCastInformationMessageAsync(string title, string content)
        {
            var senderId = Guid.Empty;
            if (_currentUser?.Id != null)
            {
                senderId = _currentUser.Id.Value;
            }

            var entity = new Notification(GuidGenerator.Create(), title, content, MessageType.BroadCast, MessageLevel.Information, senderId);
            var notificationEto = ObjectMapper.Map<Notification, NotificationEto>(entity);
            // 发送集成事件
            entity.AddCreatedNotificationLocalEvent(new CreatedNotificationLocalEvent(notificationEto));
            await _notificationRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 发送错误广播消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">消息内容</param>
        public async Task SendBroadCastErrorMessageAsync(string title, string content)
        {
            var senderId = Guid.Empty;
            if (_currentUser?.Id != null)
            {
                senderId = _currentUser.Id.Value;
            }

            var entity = new Notification(GuidGenerator.Create(), title, content, MessageType.BroadCast, MessageLevel.Error, senderId);
            var notificationEto = ObjectMapper.Map<Notification, NotificationEto>(entity);
            entity.AddCreatedNotificationLocalEvent(new CreatedNotificationLocalEvent(notificationEto));
            await _notificationRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 消息设置为已读
        /// </summary>
        /// <param name="id">消息Id</param>
        public async Task SetReadAsync(Guid id)
        {
            if (_currentUser is not { IsAuthenticated: true }) throw new AbpAuthorizationException();
            
            var notification = await _notificationRepository.FindByIdAsync(id);
            
            if (notification == null) throw new NotificationManagementDomainException(NotificationManagementErrorCodes.MessageNotExist);
            if (notification.MessageType == MessageType.BroadCast)
            {
                //如果类型是广播消息，用户设置为已读，在插入一条数据
                notification.AddBroadCastNotificationSubscription(GuidGenerator.Create(), _currentUser.GetId());
                return;
            }
            else
            {
                var notificationSubscription = notification.NotificationSubscriptions.FirstOrDefault(e => e.ReceiveId == _currentUser.GetId());
                if (notificationSubscription == null)
                    throw new NotificationManagementDomainException(NotificationManagementErrorCodes.UserUnSubscription);
                notificationSubscription.SetRead();
            }

            await _notificationRepository.UpdateAsync(notification);
        }
    }
}
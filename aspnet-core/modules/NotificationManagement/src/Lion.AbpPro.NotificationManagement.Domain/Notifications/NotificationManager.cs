using Lion.AbpPro.NotificationManagement.Notifications.Dtos;
using Lion.AbpPro.NotificationManagement.Notifications.LocalEvents;

namespace Lion.AbpPro.NotificationManagement.Notifications
{
    public class NotificationManager : NotificationManagementDomainService, INotificationManager
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
        public async Task<List<NotificationDto>> GetPagingListAsync(
            string title,
            string content,
            Guid? senderUserId,
            string senderUserName,
            Guid? receiverUserId,
            string receiverUserName,
            bool? read,
            DateTime? startReadTime,
            DateTime? endReadTime,
            MessageType? messageType,
            int maxResultCount = 10,
            int skipCount = 0)
        {
            var list = await _notificationRepository.GetPagingListAsync(title, content, senderUserId, senderUserName, receiverUserId, receiverUserName, read, startReadTime, endReadTime, messageType, maxResultCount, skipCount);
            return ObjectMapper.Map<List<Notification>, List<NotificationDto>>(list);
        }

        /// <summary>
        /// 获取消息总条数
        /// </summary>
        public async Task<long> GetPagingCountAsync(
            string title,
            string content,
            Guid? senderUserId,
            string senderUserName,
            Guid? receiverUserId,
            string receiverUserName,
            bool? read,
            DateTime? startReadTime,
            DateTime? endReadTime,
            MessageType? messageType)
        {
            return await _notificationRepository.GetPagingCountAsync(title, content, senderUserId, senderUserName, receiverUserId, receiverUserName, read, startReadTime, endReadTime, messageType);
        }

        public async Task SendCommonWarningMessageAsync(string title, string content, MessageLevel level, Guid receiveUserId, string receiveUserName)
        {
            if (!_currentUser.Id.HasValue)
            {
                throw new AbpAuthorizationException();
            }

            var entity = new Notification(GuidGenerator.Create(), title, content, MessageType.Common, level, _currentUser.Id.Value, _currentUser.UserName, receiveUserId, receiveUserName, tenantId: CurrentTenant?.Id);
            // 发送集成事件
            var notificationEto = ObjectMapper.Map<Notification, NotificationEto>(entity);
            entity.AddCreatedNotificationLocalEvent(new CreatedNotificationLocalEvent(notificationEto));
            await _notificationRepository.InsertAsync(entity);
        }

        public async Task SendBroadCastWarningMessageAsync(string title, string content, MessageLevel level)
        {
            if (!_currentUser.Id.HasValue)
            {
                throw new AbpAuthorizationException();
            }

            var entity = new Notification(GuidGenerator.Create(), title, content, MessageType.BroadCast, level, _currentUser.Id.Value, _currentUser.UserName, tenantId: CurrentTenant?.Id);
            // 发送集成事件
            var notificationEto = ObjectMapper.Map<Notification, NotificationEto>(entity);
            entity.AddCreatedNotificationLocalEvent(new CreatedNotificationLocalEvent(notificationEto));
            await _notificationRepository.InsertAsync(entity);
        }


        public async Task<NotificationDto> FindAsync(Guid id)
        {
            var notification = await _notificationRepository.FindAsync(id);
            if (notification == null) throw new NotificationManagementDomainException(NotificationManagementErrorCodes.MessageNotExist);
            return ObjectMapper.Map<Notification, NotificationDto>(notification);
        }

        public async Task<List<NotificationDto>> GetListAsync(List<Guid> ids)
        {
            var notifications = await _notificationRepository.GetListAsync(ids);
            return ObjectMapper.Map<List<Notification>, List<NotificationDto>>(notifications);
        }

        /// <summary>
        /// 消息设置为已读
        /// </summary>
        /// <param name="id">消息Id</param>
        public async Task SetReadAsync(Guid id)
        {
            if (_currentUser is not { IsAuthenticated: true }) throw new AbpAuthorizationException();

            var notification = await _notificationRepository.FindAsync(id);

            if (notification == null) throw new NotificationManagementDomainException(NotificationManagementErrorCodes.MessageNotExist);
            if (notification.Read)
            {
                return;
            }

            if (notification.MessageType == MessageType.BroadCast)
            {
                return;
            }

            notification.SetRead(true, Clock.Now);

            await _notificationRepository.UpdateAsync(notification);
        }
    }
}
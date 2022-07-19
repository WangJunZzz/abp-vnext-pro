namespace Lion.AbpPro.NotificationManagement.Notifications
{
    [Authorize]
    public class NotificationAppService : NotificationManagementAppService, INotificationAppService
    {
        private readonly NotificationManager _notificationManager;
        private readonly ICurrentUser _currentUser;


        public NotificationAppService(
            NotificationManager notificationManager,
            ICurrentUser currentUser)
        {
            _notificationManager = notificationManager;
            _currentUser = currentUser;
        }


        /// <summary>
        /// 发送警告文本消息
        /// </summary>
        public async Task SendCommonWarningMessageAsync(SendCommonMessageInput input)
        {
            await _notificationManager.SendCommonWarningMessageAsync(input.Title, input.Content, input.ReceiveIds);
        }

        /// <summary>
        /// 发送普通文本消息
        /// </summary>
        public async Task SendCommonInformationMessageAsync(SendCommonMessageInput input)
        {
            await _notificationManager.SendCommonInformationMessageAsync(input.Title, input.Content, input.ReceiveIds);
        }

        /// <summary>
        /// 发送错误文本消息
        /// </summary>
        public async Task SendCommonErrorMessageAsync(SendCommonMessageInput input)
        {
            await _notificationManager.SendCommonErrorMessageAsync(input.Title, input.Content, input.ReceiveIds);
        }

        /// <summary>
        /// 发送警告广播消息
        /// </summary>
        public async Task SendBroadCastWarningMessageAsync(SendBroadCastMessageInput input)
        {
            await _notificationManager.SendBroadCastWarningMessageAsync(input.Title, input.Content);
        }

        /// <summary>
        /// 发送正常广播消息
        /// </summary>
        public async Task SendBroadCastInformationMessageAsync(SendBroadCastMessageInput input)
        {
            await _notificationManager.SendBroadCastInformationMessageAsync(input.Title, input.Content);
        }

        /// <summary>
        /// 发送错误广播消息
        /// </summary>
        public async Task SendBroadCastErrorMessageAsync(SendBroadCastMessageInput input)
        {
            await _notificationManager.SendBroadCastErrorMessageAsync(input.Title, input.Content);
        }

        public Task SetReadAsync(SetReadInput input)
        {
            return _notificationManager.SetReadAsync(input.Id);
        }
        

        /// <summary>
        /// 分页获取用户普通文本消息
        /// </summary>
        public async Task<PagedResultDto<PagingNotificationListOutput>> GetPageCommonNotificationByUserIdAsync(PagingNotificationListInput listInput)
        {
            if (_currentUser == null || !_currentUser.Id.HasValue)
            {
                return null;
            }

            var totalCount = await _notificationManager.GetPagingCountAsync(_currentUser.Id.Value, MessageType.Common);
            var list = await _notificationManager.GetPagingListAsync(_currentUser.Id.Value, MessageType.Common, listInput.PageSize, listInput.SkipCount);
            return new PagedResultDto<PagingNotificationListOutput>(totalCount, ObjectMapper.Map<List<Notification>, List<PagingNotificationListOutput>>(list));
        }

        /// <summary>
        /// 分页获取广播消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PagingNotificationListOutput>> GetPageBroadCastNotificationByUserIdAsync(PagingNotificationListInput listInput)
        {
            var totalCount = await _notificationManager.GetPagingCountAsync(null, MessageType.Common);
            var list = await _notificationManager.GetPagingListAsync(null, MessageType.BroadCast, listInput.PageSize, listInput.SkipCount);
            return new PagedResultDto<PagingNotificationListOutput>(totalCount, ObjectMapper.Map<List<Notification>, List<PagingNotificationListOutput>>(list));
        }
    }
}
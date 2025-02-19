namespace Lion.AbpPro.NotificationManagement.Notifications
{
    [Route("Notification")]
    public class NotificationController : AbpController, INotificationAppService
    {
        private readonly INotificationAppService _notificationAppService;

        public NotificationController(
            INotificationAppService notificationAppService)
        {
            _notificationAppService = notificationAppService;
        }


      

        /// <summary>
        /// 分页获取文本消息
        /// </summary>x
        [HttpPost("NotificationPage")]
        [SwaggerOperation(summary: "分页查询消息", Tags = new[] { "Notification" })]
        public Task<PagedResultDto<PagingNotificationOutput>> PageNotificationAsync(PagingNotificationInput input)
        {
            return _notificationAppService.PageNotificationAsync(input);
        }

        /// <summary>
        /// 分页获取广播消息已读人数
        /// </summary>x
        [HttpPost("NotificationSubscriptionPage")]
        [SwaggerOperation(summary: "分页获取广播消息已读人数", Tags = new[] { "Notification" })]
        public Task<PagedResultDto<PagingNotificationSubscriptionOutput>> PageNotificationSubscriptionAsync(PagingNotificationSubscriptionInput input)
        {
            return _notificationAppService.PageNotificationSubscriptionAsync(input);
        }

        /// <summary>
        /// 删除消息
        /// </summary>x
        [HttpPost("Delete")]
        [SwaggerOperation(summary: "删除消息", Tags = new[] { "Notification" })]
        public Task DeleteAsync(DeleteMessageInput input)
        {
            return _notificationAppService.DeleteAsync(input);
        }


        [HttpPost("SendCommonWarningMessage")]
        [SwaggerOperation(summary: "发送警告文本消息", Tags = new[] { "Notification" })]
        public Task SendCommonWarningMessageAsync(SendCommonMessageInput input)
        {
            return _notificationAppService.SendCommonWarningMessageAsync(input);
        }

        [HttpPost("SendCommonInformationMessage")]
        [SwaggerOperation(summary: "发送普通文本消息", Tags = new[] { "Notification" })]
        public Task SendCommonInformationMessageAsync(SendCommonMessageInput input)
        {
            return _notificationAppService.SendCommonInformationMessageAsync(input);
        }

        [HttpPost("SendCommonErrorMessage")]
        [SwaggerOperation(summary: "发送错误文本消息", Tags = new[] { "Notification" })]
        public Task SendCommonErrorMessageAsync(SendCommonMessageInput input)
        {
            return _notificationAppService.SendCommonErrorMessageAsync(input);
        }

        [HttpPost("SendBroadCastWarningMessage")]
        [SwaggerOperation(summary: "发送警告广播消息", Tags = new[] { "Notification" })]
        public Task SendBroadCastWarningMessageAsync(SendBroadCastMessageInput input)
        {
            return _notificationAppService.SendBroadCastWarningMessageAsync(input);
        }

        [HttpPost("SendBroadCastInformationMessage")]
        [SwaggerOperation(summary: "发送正常广播消息", Tags = new[] { "Notification" })]
        public Task SendBroadCastInformationMessageAsync(SendBroadCastMessageInput input)
        {
            return _notificationAppService.SendBroadCastInformationMessageAsync(input);
        }

        [HttpPost("SendBroadCastErrorMessage")]
        [SwaggerOperation(summary: "发送错误广播消息", Tags = new[] { "Notification" })]
        public Task SendBroadCastErrorMessageAsync(SendBroadCastMessageInput input)
        {
            return _notificationAppService.SendBroadCastErrorMessageAsync(input);
        }

        [HttpPost("Read")]
        [SwaggerOperation(summary: "消息设置为已读", Tags = new[] { "Notification" })]
        public Task SetReadAsync(SetReadInput input)
        {
            return _notificationAppService.SetReadAsync(input);
        }
        
        
        [HttpPost("BatchRead")]
        [SwaggerOperation(summary: "消息批量设置为已读", Tags = new[] { "Notification" })]
        public Task SetBatchReadAsync(SetBatchReadInput input)
        {
            return _notificationAppService.SetBatchReadAsync(input);
        }
    }
}
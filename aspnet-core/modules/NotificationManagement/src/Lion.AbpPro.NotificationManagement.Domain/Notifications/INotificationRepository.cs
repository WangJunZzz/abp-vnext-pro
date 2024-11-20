namespace Lion.AbpPro.NotificationManagement.Notifications
{
    /// <summary>
    /// 消息通知 仓储接口
    /// </summary>
    public  interface INotificationRepository : IBasicRepository<Notification, Guid>
    {
        /// <summary>
        /// 分页获取消息
        /// </summary>
        Task<List<Notification>> GetPagingListAsync(
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
            MessageLevel? messageLevel,
            int maxResultCount = 10,
            int skipCount = 0,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 获取消息总条数
        /// </summary>
        Task<long> GetPagingCountAsync(
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
            MessageLevel? messageLevel,
            CancellationToken cancellationToken = default);
        
        Task<List<Notification>> GetListAsync(List<Guid> ids);
    }
}
namespace Lion.AbpPro.NotificationManagement.Notifications
{
    /// <summary>
    /// 消息通知 仓储接口
    /// </summary>
    public  interface INotificationSubscriptionRepository : IBasicRepository<NotificationSubscription, Guid>
    {
        /// <summary>
        /// 分页获取消息
        /// </summary>
        Task<List<NotificationSubscription>> GetPagingListAsync(
            Guid notificationId,
            Guid? receiverUserId,
            string receiverUserName,
            DateTime? startReadTime,
            DateTime? endReadTime,
            int maxResultCount = 10,
            int skipCount = 0,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 获取消息总条数
        /// </summary>
        Task<long> GetPagingCountAsync(  
            Guid notificationId,
            Guid? receiverUserId,
            string receiverUserName,
            DateTime? startReadTime,
            DateTime? endReadTime,
            CancellationToken cancellationToken = default);

        Task<NotificationSubscription> FindAsync(Guid receiverUserId, Guid notificationId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 分页获取消息
        /// </summary>
        Task<List<NotificationSubscription>> GetListAsync(
            List<Guid> notificationId,
            Guid receiverUserId,
            CancellationToken cancellationToken = default);
    }
}
namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore.Notifications
{
    /// <summary>
    /// 消息通知 仓储Ef core 实现
    /// </summary>
    public partial class EfCoreNotificationRepository :
        EfCoreRepository<INotificationManagementDbContext, Notification, Guid>,
        INotificationRepository
    {
        public EfCoreNotificationRepository(IDbContextProvider<INotificationManagementDbContext> dbContextProvider) :
            base(dbContextProvider)
        {
        }

        /// <summary>
        /// 查找用户消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Notification> FindByIdAsync(Guid id)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Notification>> GetPagingListAsync(
            Guid? userId,
            MessageType messageType,
            int maxResultCount = 10,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails()
                .Where(e => e.MessageType == messageType)
                .WhereIf(userId.HasValue, e => e.NotificationSubscriptions.Any(s => s.ReceiveId == userId))
                .OrderByDescending(e => e.CreationTime)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetPagingCountAsync(Guid? userId, MessageType messageType, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(e => e.MessageType == messageType)
                .WhereIf(userId.HasValue, e => e.NotificationSubscriptions.Any(s => s.ReceiveId == userId))
                .CountAsync(cancellationToken: cancellationToken);
        }


        public override async Task<IQueryable<Notification>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore.Notifications;

public class EfCoreNotificationSubscriptionRepository : EfCoreRepository<INotificationManagementDbContext, NotificationSubscription, Guid>, INotificationSubscriptionRepository
{
    public EfCoreNotificationSubscriptionRepository([NotNull] IDbContextProvider<INotificationManagementDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }


    public async Task<List<NotificationSubscription>> GetPagingListAsync(Guid notificationId, Guid? receiverUserId, string receiverUserName, DateTime? startReadTime, DateTime? endReadTime, int maxResultCount = 10, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(e => e.NotificationId == notificationId)
            .WhereIf(receiverUserId.HasValue, e => e.ReceiveUserId == receiverUserId.Value)
            .WhereIf(receiverUserName.IsNotNullOrWhiteSpace(), e => e.ReceiveUserName == receiverUserName)
            .WhereIf(startReadTime.HasValue, e => e.ReadTime >= startReadTime.Value)
            .WhereIf(endReadTime.HasValue, e => e.ReadTime <= endReadTime.Value)
            .OrderByDescending(e => e.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<long> GetPagingCountAsync(Guid notificationId, Guid? receiverUserId, string receiverUserName, DateTime? startReadTime, DateTime? endReadTime, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(e => e.NotificationId == notificationId)
            .WhereIf(receiverUserId.HasValue, e => e.ReceiveUserId == receiverUserId.Value)
            .WhereIf(receiverUserName.IsNotNullOrWhiteSpace(), e => e.ReceiveUserName == receiverUserName)
            .WhereIf(startReadTime.HasValue, e => e.ReadTime >= startReadTime.Value)
            .WhereIf(endReadTime.HasValue, e => e.ReadTime <= endReadTime.Value)
            .OrderByDescending(e => e.CreationTime)
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<NotificationSubscription> FindAsync(Guid receiverUserId, Guid notificationId, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync()).FirstOrDefaultAsync(e => e.ReceiveUserId == receiverUserId && e.NotificationId == notificationId, GetCancellationToken(cancellationToken));
    }
}
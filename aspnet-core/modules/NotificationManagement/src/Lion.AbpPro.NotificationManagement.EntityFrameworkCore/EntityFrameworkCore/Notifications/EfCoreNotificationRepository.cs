using Lion.AbpPro.SignalR.Enums;

namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore.Notifications
{
    /// <summary>
    /// 消息通知 仓储Ef core 实现
    /// </summary>
    public class EfCoreNotificationRepository :
        EfCoreRepository<INotificationManagementDbContext, Notification, Guid>,
        INotificationRepository
    {
        public EfCoreNotificationRepository(IDbContextProvider<INotificationManagementDbContext> dbContextProvider) :
            base(dbContextProvider)
        {
        }

        public async Task<List<Notification>> GetPagingListAsync(
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
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(title.IsNotNullOrWhiteSpace(), e => e.Title.Contains(title))
                .WhereIf(content.IsNotNullOrWhiteSpace(), e => e.Content.Contains(content))
                .WhereIf(senderUserId.HasValue, e => e.SenderUserId == senderUserId.Value)
                .WhereIf(senderUserName.IsNotNullOrWhiteSpace(), e => e.SenderUserName == senderUserName)
                .WhereIf(receiverUserId.HasValue, e => e.ReceiveUserId == receiverUserId.Value)
                .WhereIf(receiverUserName.IsNotNullOrWhiteSpace(), e => e.ReceiveUserName == receiverUserName)
                .WhereIf(read.HasValue, e => e.Read == read.Value)
                .WhereIf(startReadTime.HasValue, e => e.ReadTime >= startReadTime.Value)
                .WhereIf(endReadTime.HasValue, e => e.ReadTime <= endReadTime.Value)
                .WhereIf(messageType.HasValue, e => e.MessageType == messageType.Value)
                .WhereIf(messageLevel.HasValue, e => e.MessageLevel == messageLevel.Value)
                .OrderByDescending(e => e.CreationTime)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

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
            MessageType? messageType,
            MessageLevel? messageLevel,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(title.IsNotNullOrWhiteSpace(), e => e.Title.Contains(title))
                .WhereIf(content.IsNotNullOrWhiteSpace(), e => e.Content.Contains(content))
                .WhereIf(senderUserId.HasValue, e => e.SenderUserId == senderUserId.Value)
                .WhereIf(senderUserName.IsNotNullOrWhiteSpace(), e => e.SenderUserName == senderUserName)
                .WhereIf(receiverUserId.HasValue, e => e.ReceiveUserId == receiverUserId.Value)
                .WhereIf(receiverUserName.IsNotNullOrWhiteSpace(), e => e.ReceiveUserName == receiverUserName)
                .WhereIf(read.HasValue, e => e.Read == read.Value)
                .WhereIf(startReadTime.HasValue, e => e.ReadTime >= startReadTime.Value)
                .WhereIf(endReadTime.HasValue, e => e.ReadTime <= endReadTime.Value)
                .WhereIf(messageType.HasValue, e => e.MessageType == messageType.Value)
                .WhereIf(messageLevel.HasValue, e => e.MessageLevel == messageLevel.Value)
                .CountAsync(cancellationToken);
        }

        public async Task<List<Notification>> GetListAsync(List<Guid> ids)
        {
            return await (await GetDbSetAsync()).Where(e => ids.Contains(e.Id)).ToListAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.ProjectName.NotificationManagement.Notifications;
using CompanyName.ProjectName.NotificationManagement.Notifications.Dtos;
using Dapper;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace CompanyName.ProjectName.NotificationManagement.EntityFrameworkCore.Notifications
{
    public class DapperNotificationRepository : DapperRepository<INotificationManagementDbContext>,
        IDapperNotificationRepository
    {
        public DapperNotificationRepository(
            IDbContextProvider<INotificationManagementDbContext> dbContextProvider) :
            base(dbContextProvider)
        {
        }

        /// <summary>
        /// 分页查询广播消息
        /// </summary>
        /// <returns></returns>
        public async Task<List<PagingNotificationListOutput>>
            GetPageBroadCastNotificationByUserIdAsync(
                Guid userId,
                int maxResultCount = 10,
                int skipCount = 0,
                CancellationToken cancellationToken = default)
        {
            var sql = BuildPageBroadCastSql();
            sql += $" LIMIT {maxResultCount} OFFSET {skipCount}";
            var tran = await GetDbTransactionAsync();
            return (await (await GetDbConnectionAsync()).QueryAsync<PagingNotificationListOutput>(
                    sql, new { userId },
                    transaction: tran))
                .ToList();
        }

        /// <summary>
        /// 获取广播消息总条数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetPageBroadCastNotificationCountByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var sql = BuildPageBroadCastCountSql();
            var tran = await GetDbTransactionAsync();
            return (await (await GetDbConnectionAsync()).QueryAsync<int>(sql, new { userId },
                    transaction: tran))
                .FirstOrDefault();
        }

        /// <summary>
        /// 分页查询文本消息
        /// </summary>
        /// <returns></returns>
        public async Task<List<PagingNotificationListOutput>> GetPageTextNotificationByUserIdAsync(
            Guid userId,
            int maxResultCount = 10,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var sql = BuildPageTextSql();
            sql += $" LIMIT {maxResultCount} OFFSET {skipCount}";
            var tran = await GetDbTransactionAsync();
            return (await (await GetDbConnectionAsync()).QueryAsync<PagingNotificationListOutput>(
                    sql, new { userId },
                    transaction: tran))
                .ToList();
        }

        /// <summary>
        /// 获取文本息总条数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetPageTextNotificationCountByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var sql = BuildPageTextCountSql();
            var tran = await GetDbTransactionAsync();
            return (await (await GetDbConnectionAsync()).QueryAsync<int>(sql, new { userId },
                    transaction: tran))
                .FirstOrDefault();
        }

        private string BuildPageTextSql()
        {
            return "select "
                   + "a.Id,"
                   + "a.Title,"
                   + "a.Content,"
                   + "a.CreationTime, "
                   + "b.Read "
                   + $"from {NotificationManagementDbProperties.DbTablePrefix}Notification a  "
                   + $"left join {NotificationManagementDbProperties.DbTablePrefix}NotificationSubscription b on b.NotificationId=a.Id "
                   + "where a.IsDeleted=0  "
                   + "and a.MessageType=20 "
                   + "and b.ReceiveId=?userId "
                   + "order by  b.Read,  CreationTime desc ";
        }

        private string BuildPageTextCountSql()
        {
            return "select "
                   + "count(1) as count "
                   + "from Notification a "
                   + $"left join {NotificationManagementDbProperties.DbTablePrefix}NotificationSubscription b on b.NotificationId=a.Id "
                   + "where a.IsDeleted=0  "
                   + "and a.MessageType=20 "
                   + "and b.ReceiveId=?userId ";
        }

        private string BuildPageBroadCastCountSql()
        {
            return "select count(1) as count from ("
                   + "select a.Id, a.Title, a.Content, a.CreationTime, a.SenderId, false as \"Read\" "
                   + $"from {NotificationManagementDbProperties.DbTablePrefix}Notification a "
                   + "where a.IsDeleted = 0 "
                   + "and a.MessageType = 10 "
                   + "and a.Id not in "
                   + "    (select NotificationId "
                   + $"from {NotificationManagementDbProperties.DbTablePrefix}NotificationSubscription b "
                   + " where b.ReceiveId = ?userId) "
                   + "union "
                   + "    select a.Id, a.Title, a.Content, a.CreationTime, a.SenderId, true as \"Read\" "
                   + $"from {NotificationManagementDbProperties.DbTablePrefix}Notification a "
                   + " where a.IsDeleted = 0 "
                   + " and a.MessageType = 10 "
                   + "and a.Id in "
                   + $"    (select {NotificationManagementDbProperties.DbTablePrefix}NotificationId "
                   + $"from {NotificationManagementDbProperties.DbTablePrefix}NotificationSubscription b "
                   + "where b.ReceiveId = ?userId) "
                   + "    ) as tt  ";
        }

        private string BuildPageBroadCastSql()
        {
            return "select * from ("
                   + "select a.Id, a.Title, a.Content, a.CreationTime, a.SenderId, false as \"Read\" "
                   + $"from {NotificationManagementDbProperties.DbTablePrefix}Notification a "
                   + "where a.IsDeleted = 0 "
                   + "and a.MessageType = 10 "
                   + "and a.Id not in "
                   + "    (select NotificationId "
                   + $"from {NotificationManagementDbProperties.DbTablePrefix}NotificationSubscription b "
                   + " where b.ReceiveId = ?userId) "
                   + "union"
                   + "    select a.Id, a.Title, a.Content, a.CreationTime, a.SenderId, true as \"Read\" "
                   + $"from  {NotificationManagementDbProperties.DbTablePrefix}Notification a "
                   + " where a.IsDeleted = 0 "
                   + " and a.MessageType = 10 "
                   + "and a.Id in "
                   + "    (select NotificationId "
                   + $"from  {NotificationManagementDbProperties.DbTablePrefix}NotificationSubscription b "
                   + "where b.ReceiveId = ?userId)"
                   + "    ) as tt order by tt.Read,tt.CreationTime  ";
        }
    }
}
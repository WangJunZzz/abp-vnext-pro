using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lion.AbpPro.NotificationManagement.Notifications.Aggregates;
using Lion.AbpPro.NotificationManagement.Notifications.Enums;
using Volo.Abp.Domain.Repositories;

namespace Lion.AbpPro.NotificationManagement.Notifications
{
    /// <summary>
    /// 消息通知 仓储接口
    /// </summary>
    public partial interface INotificationRepository : IBasicRepository<Notification, Guid>
    {
        /// <summary>
        /// 查找用户消息
        /// </summary>
        /// <param name="id"></param>
        Task<Notification> FindByIdAsync(Guid id);

        /// <summary>
        /// 分页获取消息
        /// </summary>
        Task<List<Notification>> GetPagingListAsync(
            Guid? userId,
            MessageType messageType,
            int maxResultCount = 10,
            int skipCount = 0,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 获取消息总条数
        /// </summary>
        Task<long> GetPagingCountAsync(Guid? userId, MessageType messageType, CancellationToken cancellationToken = default);
    }
}
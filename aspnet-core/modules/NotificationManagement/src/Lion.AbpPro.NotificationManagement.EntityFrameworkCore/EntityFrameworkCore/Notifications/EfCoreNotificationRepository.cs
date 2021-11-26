using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Lion.AbpPro.NotificationManagement.Notifications;

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
    }
}
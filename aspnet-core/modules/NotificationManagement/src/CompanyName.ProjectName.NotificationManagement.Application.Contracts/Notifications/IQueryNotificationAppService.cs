using System.Threading;
using System.Threading.Tasks;
using CompanyName.ProjectName.NotificationManagement.Notifications.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    public interface IQueryNotificationAppService : IApplicationService
    {
        /// <summary>
        /// 分页获取用户普通文本消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedResultDto<PagingNotificationListOutput>> GetPageTextNotificationByUserIdAsync(
            PagingNotificationListInput listInput,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页获取广播消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedResultDto<PagingNotificationListOutput>> GetPageBroadCastNotificationByUserIdAsync(
            PagingNotificationListInput listInput,
            CancellationToken cancellationToken = default);
    }
}
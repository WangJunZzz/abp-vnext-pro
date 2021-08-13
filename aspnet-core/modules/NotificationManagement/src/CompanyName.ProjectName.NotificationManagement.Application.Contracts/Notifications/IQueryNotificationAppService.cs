// using System.Threading;
// using System.Threading.Tasks;
// using Volo.Abp.Application.Services;
// using Volo.Abp.Application.Dtos;
//
// namespace CompanyName.ProjectName.NotificationManagement.Notifications
// {
//     public interface IQueryNotificationAppService : IApplicationService
//     {
//         /// <summary>
//         /// 分页获取用户普通文本消息
//         /// </summary>
//         /// <param name="input"></param>
//         /// <param name="cancellationToken"></param>
//         /// <returns></returns>
//         Task<PagedResultDto<QueryTextNotificationOutput>> GetPageTextNotificationByUserIdAsync(
//             QueryTextNotificationInput input,
//             CancellationToken cancellationToken = default);
//
//         /// <summary>
//         /// 分页获取广播消息
//         /// </summary>
//         /// <param name="input"></param>
//         /// <param name="cancellationToken"></param>
//         /// <returns></returns>
//         Task<PagedResultDto<QueryTextNotificationOutput>> GetPageBroadCastNotificationByUserIdAsync(
//             QueryTextNotificationInput input,
//             CancellationToken cancellationToken = default);
//     }
// }
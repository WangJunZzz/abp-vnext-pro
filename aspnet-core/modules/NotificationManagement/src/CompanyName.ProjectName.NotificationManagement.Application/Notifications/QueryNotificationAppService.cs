// using System.Threading;
// using System.Threading.Tasks;
// using Bee.Abp.Dto;
// using Humanizer;
// using CompanyName.ProjectName.QueryManagement.Notifications;
//
// namespace CompanyName.ProjectName.NotificationManagement.Notifications
// {
//     public class QueryNotificationAppService : NotificationManagementAppService,IQueryNotificationAppService
//     {
//         private readonly INotificationFreeSqlRepository _notificationFreeSqlRepository;
//
//         public QueryNotificationAppService(INotificationFreeSqlRepository notificationFreeSqlRepository)
//         {
//             _notificationFreeSqlRepository = notificationFreeSqlRepository;
//         }
//
//         /// <summary>
//         /// 分页获取用户普通文本消息
//         /// </summary>
//         /// <param name="input"></param>
//         /// <param name="cancellationToken"></param>
//         /// <returns></returns>
//         public  Task<BeePagedResultDto<QueryTextNotificationOutput>> GetPageTextNotificationByUserIdAsync(
//             QueryTextNotificationInput input,
//             CancellationToken cancellationToken = default)
//         {
//             return  _notificationFreeSqlRepository.GetPageTextNotificationByUserIdAsync(input, cancellationToken);
//         }
//
//         /// <summary>
//         /// 分页获取广播消息
//         /// </summary>
//         /// <param name="input"></param>
//         /// <param name="cancellationToken"></param>
//         /// <returns></returns>
//         public Task<BeePagedResultDto<QueryTextNotificationOutput>> GetPageBroadCastNotificationByUserIdAsync(
//             QueryTextNotificationInput input,
//             CancellationToken cancellationToken = default)
//         {
//             return _notificationFreeSqlRepository.GetPageBroadCastNotificationByUserIdAsync(input, cancellationToken);
//         }
//     }
// }
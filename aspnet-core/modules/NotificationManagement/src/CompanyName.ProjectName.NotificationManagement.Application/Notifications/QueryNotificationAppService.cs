using System.Threading;
using System.Threading.Tasks;
using CompanyName.ProjectName.NotificationManagement.Notifications.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;


namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    public class QueryNotificationAppService : NotificationManagementAppService, IQueryNotificationAppService
    {
        private readonly IDapperNotificationRepository _dapperNotificationRepository;
        private readonly ICurrentUser _currentUser;

        public QueryNotificationAppService(IDapperNotificationRepository dapperNotificationRepository, ICurrentUser currentUser)
        {
            _dapperNotificationRepository = dapperNotificationRepository;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 分页获取用户普通文本消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PagingNotificationListOutput>> GetPageTextNotificationByUserIdAsync(
            PagingNotificationListInput listInput,
            CancellationToken cancellationToken = default)
        {
            if (!_currentUser.Id.HasValue)
            {
                return null;
            }

            var totalCount =
                await _dapperNotificationRepository.GetPageTextNotificationCountByUserIdAsync(_currentUser.Id.Value, cancellationToken);
            var list = await _dapperNotificationRepository.GetPageTextNotificationByUserIdAsync(_currentUser.Id.Value,
                listInput.PageSize,
                listInput.SkipCount, cancellationToken);
            return new PagedResultDto<PagingNotificationListOutput>(totalCount, list);
        }

        /// <summary>
        /// 分页获取广播消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PagingNotificationListOutput>> GetPageBroadCastNotificationByUserIdAsync(
            PagingNotificationListInput listInput,
            CancellationToken cancellationToken = default)
        {
            if (!_currentUser.Id.HasValue)
            {
                return null;
            }

            var totalCount =
                await _dapperNotificationRepository.GetPageBroadCastNotificationCountByUserIdAsync(_currentUser.Id.Value,
                    cancellationToken);
            var list = await _dapperNotificationRepository.GetPageBroadCastNotificationByUserIdAsync(_currentUser.Id.Value,
                listInput.PageSize,
                listInput.SkipCount, cancellationToken);
            return new PagedResultDto<PagingNotificationListOutput>(totalCount, list);
        }
    }
}
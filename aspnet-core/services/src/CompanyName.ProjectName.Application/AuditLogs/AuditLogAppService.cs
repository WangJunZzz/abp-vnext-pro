using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AuditLogging;

namespace CompanyName.ProjectName.AuditLogs
{
    public class AuditLogAppService : ProjectNameAppService, IAuditLogAppService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogAppService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// 分页查询审计日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetAuditLogPageListOutput>> GetListAsync(PagingAuditLogListInput input)
        {
            var list = await _auditLogRepository.GetListAsync(
                input.Sorting,
                input.PageSize,
                input.SkipCount,
                input.StartTime?.Date,
                input.EndTime?.Date,
                input.HttpMethod,
                input.Url,
                null,
                input.UserName,
                input.ApplicationName,
                input.CorrelationId,
                input.MaxExecutionDuration,
                input.MinExecutionDuration,
                input.HasException,
                input.HttpStatusCode);
            var totalCount = await _auditLogRepository.GetCountAsync(
                input.StartTime?.Date,
                input.EndTime?.Date,
                input.HttpMethod,
                input.Url,
                null,
                input.UserName,
                input.ApplicationName,
                input.CorrelationId,
                input.MaxExecutionDuration,
                input.MinExecutionDuration,
                input.HasException,
                input.HttpStatusCode);
            return new PagedResultDto<GetAuditLogPageListOutput>(totalCount,
                ObjectMapper.Map<List<AuditLog>, List<GetAuditLogPageListOutput>>(list));
        }
    }
}
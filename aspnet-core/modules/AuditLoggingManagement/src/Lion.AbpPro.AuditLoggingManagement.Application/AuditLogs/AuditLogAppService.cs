using Lion.AbpPro.AuditLoggingManagement.AuditLogs.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AuditLogging;

namespace Lion.AbpPro.AuditLoggingManagement.AuditLogs
{
    public class AuditLogAppService : AuditLoggingManagementAppServiceBase, IAuditLogAppService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogAppService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<ListResultDto<AuditLogOutput>> GetPageListAsync(GetAuditLogsInput input, CancellationToken cancellationToken = default)
        {
            var auditLogs = await _auditLogRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.StartExecutionTime,
                input.EndExecutionTime,
                input.HttpMethod,
                input.Url,
                input.UserId,
                input.UserName,
                input.ApplicationName,
                input.ClientIpAddress,
                input.CorrelationId,
                input.MaxExecutionDuration,
                input.MinExecutionDuration,
                input.HasException,
                input.HttpStatusCode,
                input.IncludeDetails,
                cancellationToken
                );

            var datas = ObjectMapper.Map<List<AuditLog>, List<AuditLogOutput>>(auditLogs);
            return new ListResultDto<AuditLogOutput>(datas);
        }

        public async Task<AuditLogOutput> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var auditLog = await _auditLogRepository.FindAsync(model => model.Id == id, cancellationToken: cancellationToken);
            if (null == auditLog)
            {
                throw new UserFriendlyException(L["AuditLogNotFound", id]);
            }

            return ObjectMapper.Map<AuditLog, AuditLogOutput>(auditLog);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var auditLog = await _auditLogRepository.FindAsync(model => model.Id == id, cancellationToken: cancellationToken);
            if (null == auditLog)
            {
                throw new UserFriendlyException(L["AuditLogNotFound", id]);
            }

            await _auditLogRepository.DeleteAsync(auditLog, cancellationToken: cancellationToken);
        }

        public async Task<ListResultDto<EntityChangeOutput>> GetEntityChangePageListAsync(GetEntityChangesInput input, CancellationToken cancellationToken = default)
        {
            var entityChange = await _auditLogRepository.GetEntityChangeListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.AuditLogId,
                input.StartTime,
                input.SndTime,
                input.ChangeType,
                input.EntityId,
                input.EntityTypeFullName,
                input.IncludeDetails,
                cancellationToken);

            var datas = ObjectMapper.Map<List<EntityChange>, List<EntityChangeOutput>>(entityChange);

            return new ListResultDto<EntityChangeOutput>(datas);
        }
    }
}

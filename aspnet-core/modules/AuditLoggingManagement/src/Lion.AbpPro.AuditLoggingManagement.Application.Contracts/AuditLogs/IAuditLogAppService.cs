using Lion.AbpPro.AuditLoggingManagement.AuditLogs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.AuditLoggingManagement.AuditLogs
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<ListResultDto<AuditLogOutput>> GetPageListAsync(GetAuditLogsInput input, CancellationToken cancellationToken = default);

        Task<ListResultDto<EntityChangeOutput>> GetEntityChangePageListAsync(GetEntityChangesInput input, CancellationToken cancellationToken = default);

        Task<AuditLogOutput> GetAsync(Guid id, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

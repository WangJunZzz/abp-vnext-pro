using Lion.AbpPro.AuditLoggingManagement.AuditLogs.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Lion.AbpPro.AuditLoggingManagement.AuditLogs
{
    [Route("api/auditlog-management/auditlogs")]
    public class AuditLogController : AbpController, IAuditLogAppService
    {
        private IAuditLogAppService _auditLogAppService;

        public AuditLogController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        [HttpPost]
        [Route("delete/{id}")]
        [SwaggerOperation(summary: "通过审计日志Id删除审计日志", Tags = new[] { "Auditlogs" })]
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _auditLogAppService.DeleteAsync(id, cancellationToken);
        }

        [HttpPost]
        [Route("page")]
        [SwaggerOperation(summary: "分页获取审计日志", Tags = new[] { "Auditlogs" })]
        public async Task<ListResultDto<AuditLogOutput>> GetPageListAsync(GetAuditLogsInput input, CancellationToken cancellationToken = default)
        {
            return await _auditLogAppService.GetPageListAsync(input, cancellationToken);
        }

        [HttpPost]
        [Route("detail/{id}")]
        [SwaggerOperation(summary: "通过审计日志Id查找审计日志详情", Tags = new[] { "Auditlogs" })]
        public async Task<AuditLogOutput> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _auditLogAppService.GetAsync(id, cancellationToken);
        }

        [HttpPost]
        [Route("page-entity-changes")]
        [SwaggerOperation(summary: "查询实体变化", Tags = new[] { "Auditlogs" })]
        public async Task<ListResultDto<EntityChangeOutput>> GetEntityChangePageListAsync(GetEntityChangesInput input, CancellationToken cancellationToken = default)
        {
            return await _auditLogAppService.GetEntityChangePageListAsync(input, cancellationToken);
        }
    }
}

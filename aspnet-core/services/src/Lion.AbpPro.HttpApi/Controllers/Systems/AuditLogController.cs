using Lion.AbpPro.AuditLogs;
using Lion.AbpPro.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.Controllers.Systems
{
    [Route("AuditLogs")]
    public class AuditLogController : AbpProController,IAuditLogAppService
    {
        private readonly IAuditLogAppService _auditLogAppService;

        public AuditLogController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取审计日志信息", Tags = new[] {"AuditLogs"})]
        public Task<PagedResultDto<GetAuditLogPageListOutput>> GetListAsync(PagingAuditLogListInput input)
        {
            return _auditLogAppService.GetListAsync(input);
        }
    }
}
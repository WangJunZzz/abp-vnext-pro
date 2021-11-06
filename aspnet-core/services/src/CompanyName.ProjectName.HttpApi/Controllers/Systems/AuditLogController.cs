using System.Threading.Tasks;
using CompanyName.ProjectName.AuditLogs;
using CompanyName.ProjectName.Permissions;
using CompanyName.ProjectName.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace CompanyName.ProjectName.Controllers.Systems
{
    [Route("AuditLogs")]
    [Authorize(Policy = IdentityPermissions.Users.Default)]
    public class AuditLogController : ProjectNameController,IAuditLogAppService
    {
        private readonly IAuditLogAppService _auditLogAppService;

        public AuditLogController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        [HttpPost("page")]
        [Authorize(Policy = ProjectNamePermissions.SystemManagement.AuditLog)]
        [SwaggerOperation(summary: "分页获取用户信息", Tags = new[] {"AuditLogs"})]
        public Task<PagedResultDto<GetAuditLogPageListOutput>> GetListAsync(PagingAuditLogListInput input)
        {
            return _auditLogAppService.GetListAsync(input);
        }
    }
}
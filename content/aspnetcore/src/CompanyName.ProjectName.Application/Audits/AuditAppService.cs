using CompanyNameProjectName.Audits.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Repositories;


namespace CompanyNameProjectName.Audits
{
    [Authorize("AbpIdentity.Users.AuditLog")]
    public class AuditAppService : ApplicationService
    {
        private readonly IRepository<AuditLog, Guid> _auditLogRepository;

        public AuditAppService(IRepository<AuditLog, Guid> auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// 分页查询审计日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [SwaggerOperation(summary: "分页获取审计日志信息", Tags = new[] { "Audit" })]
        public async Task<PagedResultDto<QueryAuditLogOutput>> ListAsync(QueryAuditLogInput input)
        {

            var query = _auditLogRepository
                       .WhereIf(!input.ExecutionTime.IsNullOrWhiteSpace(), e => e.ExecutionTime > Convert.ToDateTime(input.ExecutionTime).AddSeconds(-1))
                       .WhereIf(!input.ExecutionTime.IsNullOrWhiteSpace(), e => e.ExecutionTime < Convert.ToDateTime(input.ExecutionTime).AddDays(1).AddSeconds(-1))
                       .WhereIf(!input.UserName.IsNullOrWhiteSpace(), e => e.UserName == input.UserName.Trim())
                       .WhereIf(!input.HttpMethod.IsNullOrWhiteSpace(), e => e.HttpMethod == input.HttpMethod)
                       .WhereIf(input.HttpStatusCode > 0, e => e.HttpStatusCode == input.HttpStatusCode);

            var totalCount = await query.CountAsync();
            var maxResultCount = input.PageSize;
            var skipCount = (input.PageIndex - 1) * input.PageSize;
            var list = await query.OrderByDescending(e => e.ExecutionTime).PageBy(skipCount, maxResultCount).ToListAsync();
            return new PagedResultDto<QueryAuditLogOutput>(
                totalCount,
                ObjectMapper.Map<List<AuditLog>, List<QueryAuditLogOutput>>(list)
                );
        }

        /// <summary>
        /// 查询审计日志的实体变化信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [SwaggerOperation(summary: "获取审计日志详情", Tags = new[] { "Audit" })]
        public async Task<List<QueryEntityChangeOutput>> QueryEntity(QueryEntityChangeInput input)
        {
            var entity = await _auditLogRepository.IncludeDetails().FirstOrDefaultAsync(e => e.Id == input.Id);
            if (entity != null)
            {
                return ObjectMapper.Map<List<EntityChange>, List<QueryEntityChangeOutput>>(entity.EntityChanges.ToList());
            }
            return null;
        }
    }
}

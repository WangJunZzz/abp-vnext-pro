using System.Threading.Tasks;
using CompanyName.ProjectName.Tenants.Dtos;
using Lion.Abp.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace CompanyName.ProjectName.Controllers.Tenants
{
    [Route("Tenants")]
    [Authorize(TenantManagementPermissions.Tenants.Default)]
    public class TenantController : ProjectNameController
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取租户信息", Tags = new[] {"Tenants"})]
        public Task<PagedResultDto<TenantDto>> ListAsync(PagingTenantInput input)
        {
            var request = new GetTenantsInput {Filter = input.Filter, SkipCount = input.SkipCount, MaxResultCount = input.PageSize};
            return _tenantAppService.GetListAsync(request);
        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "创建租户", Tags = new[] {"Tenants"})]
        [Authorize(TenantManagementPermissions.Tenants.Create)]
        public Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            return _tenantAppService.CreateAsync(input);
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "更新租户", Tags = new[] {"Tenants"})]
        [Authorize(TenantManagementPermissions.Tenants.Update)]
        public Task<TenantDto> UpdateAsync(UpdateTenantInput input)
        {
            var request = new TenantUpdateDto()
            {
                Name = input.Name.Trim()
            };
            return _tenantAppService.UpdateAsync(input.Id, request);
        }
        
        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除租户", Tags = new[] {"Tenants"})]
        [Authorize(TenantManagementPermissions.Tenants.Delete)]
        public Task DeleteAsync(IdInput input)
        {
            return _tenantAppService.DeleteAsync(input.Id);
        }

        [HttpPost("getConnectionString")]
        [SwaggerOperation(summary: "获取租户连接字符串", Tags = new[] {"Tenants"})]
        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public Task<string> GetDefaultConnectionStringAsync(IdInput input)
        {
            return _tenantAppService.GetDefaultConnectionStringAsync(input.Id);
        }

        [HttpPost("updateConnectionString")]
        [SwaggerOperation(summary: "更新租户连接字符串", Tags = new[] {"Tenants"})]
        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public Task UpdateDefaultConnectionStringAsync(UpdateConnectionStringInput input)
        {
            return _tenantAppService.UpdateDefaultConnectionStringAsync(input.Id, input.ConnectionString);
        }

        [HttpPost("deleteConnectionString")]
        [SwaggerOperation(summary: "删除租户连接字符串", Tags = new[] {"Tenants"})]
        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public Task DeleteDefaultConnectionStringAsync(IdInput input)
        {
            return _tenantAppService.DeleteDefaultConnectionStringAsync(input.Id);
        }
    }
}
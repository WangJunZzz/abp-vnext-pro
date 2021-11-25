using System.Threading.Tasks;
using CompanyName.ProjectName.Extension.Customs.Dtos;
using CompanyName.ProjectName.Tenants.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace CompanyName.ProjectName.Tenants
{
    public class VoloTenantAppService : ProjectNameAppService, IVoloTenantAppService
    {
        private readonly IAbpTenantAppService _abpTenantAppService;
        private readonly ITenantAppService _tenantAppService;

        public VoloTenantAppService(
            IAbpTenantAppService abpTenantAppService, 
            ITenantAppService tenantAppService)
        {
            _abpTenantAppService = abpTenantAppService;
            _tenantAppService = tenantAppService;
        }
        
        public async Task<FindTenantResultDto> FindTenantByNameAsync(FindTenantByNameInput input)
        {
            return await _abpTenantAppService.FindTenantByNameAsync(input.Name);
        }

        
        [Authorize(policy:TenantManagementPermissions.Tenants.Default)]
        public Task<PagedResultDto<TenantDto>> ListAsync(PagingTenantInput input)
        {
            var request = new GetTenantsInput
            {
                Filter = input.Filter, SkipCount = input.SkipCount, MaxResultCount = input.PageSize
            };
            return _tenantAppService.GetListAsync(request);
        }

        [Authorize(policy:TenantManagementPermissions.Tenants.Create)]
        public Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            return _tenantAppService.CreateAsync(input);
        }

        [Authorize(policy:TenantManagementPermissions.Tenants.Update)]
        public Task<TenantDto> UpdateAsync(UpdateTenantInput input)
        {
            var request = new TenantUpdateDto()
            {
                Name = input.Name.Trim()
            };
            return _tenantAppService.UpdateAsync(input.Id, request);
        }

        [Authorize(policy:TenantManagementPermissions.Tenants.Delete)]
        public Task DeleteAsync(IdInput input)
        {
            return _tenantAppService.DeleteAsync(input.Id);
        }

        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public Task<string> GetDefaultConnectionStringAsync(IdInput input)
        {
            return _tenantAppService.GetDefaultConnectionStringAsync(input.Id);
        }

        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public Task UpdateDefaultConnectionStringAsync(UpdateConnectionStringInput input)
        {
            return _tenantAppService.UpdateDefaultConnectionStringAsync(input.Id,
                input.ConnectionString);
        }

        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public Task DeleteDefaultConnectionStringAsync(IdInput input)
        {
            return _tenantAppService.DeleteDefaultConnectionStringAsync(input.Id);
        }
    }
}
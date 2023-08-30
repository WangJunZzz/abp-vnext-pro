namespace Lion.AbpPro.BasicManagement.Tenants
{
    [Authorize(TenantManagementPermissions.Tenants.Default)]
    public class VoloTenantAppService : BasicManagementAppService, IVoloTenantAppService
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
        
        [AllowAnonymous]
        public virtual async Task<FindTenantResultDto> FindTenantByNameAsync(FindTenantByNameInput input)
        {
            return await _abpTenantAppService.FindTenantByNameAsync(input.Name);
        }

        
        public virtual Task<PagedResultDto<TenantDto>> ListAsync(PagingTenantInput input)
        {
            var request = new GetTenantsInput
            {
                Filter = input.Filter, SkipCount = input.SkipCount, MaxResultCount = input.PageSize
            };
            return _tenantAppService.GetListAsync(request);
        }

        [Authorize(policy:TenantManagementPermissions.Tenants.Create)]
        public virtual Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            return _tenantAppService.CreateAsync(input);
        }

        [Authorize(policy:TenantManagementPermissions.Tenants.Update)]
        public virtual Task<TenantDto> UpdateAsync(UpdateTenantInput input)
        {
            var request = new TenantUpdateDto()
            {
                Name = input.Name.Trim()
            };
            return _tenantAppService.UpdateAsync(input.Id, request);
        }

        [Authorize(policy:TenantManagementPermissions.Tenants.Delete)]
        public virtual Task DeleteAsync(IdInput input)
        {
            return _tenantAppService.DeleteAsync(input.Id);
        }

        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public virtual Task<string> GetDefaultConnectionStringAsync(IdInput input)
        {
            return _tenantAppService.GetDefaultConnectionStringAsync(input.Id);
        }

        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public virtual Task UpdateDefaultConnectionStringAsync(UpdateConnectionStringInput input)
        {
            return _tenantAppService.UpdateDefaultConnectionStringAsync(input.Id,
                input.ConnectionString);
        }

        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public virtual Task DeleteDefaultConnectionStringAsync(IdInput input)
        {
            return _tenantAppService.DeleteDefaultConnectionStringAsync(input.Id);
        }
    }
}
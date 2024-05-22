using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.SettingManagement;

namespace Lion.AbpPro.BasicManagement.Tenants
{
    [Authorize(TenantManagementPermissions.Tenants.Default)]
    public class VoloTenantAppService : BasicManagementAppService, IVoloTenantAppService
    {
        private readonly IAbpTenantAppService _abpTenantAppService;
        private readonly ITenantAppService _tenantAppService;
        private readonly ITenantRepository _tenantRepository;

        public VoloTenantAppService(
            IAbpTenantAppService abpTenantAppService,
            ITenantAppService tenantAppService,
            ITenantRepository tenantRepository)
        {
            _abpTenantAppService = abpTenantAppService;
            _tenantAppService = tenantAppService;
            _tenantRepository = tenantRepository;
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

        [Authorize(policy: TenantManagementPermissions.Tenants.Create)]
        public virtual Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            return _tenantAppService.CreateAsync(input);
        }

        [Authorize(policy: TenantManagementPermissions.Tenants.Update)]
        public virtual Task<TenantDto> UpdateAsync(UpdateTenantInput input)
        {
            var request = new TenantUpdateDto()
            {
                Name = input.Name.Trim()
            };
            return _tenantAppService.UpdateAsync(input.Id, request);
        }

        [Authorize(policy: TenantManagementPermissions.Tenants.Delete)]
        public virtual Task DeleteAsync(IdInput input)
        {
            return _tenantAppService.DeleteAsync(input.Id);
        }
        

        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public async Task<PagedResultDto<PageTenantConnectionStringOutput>> PageConnectionStringsAsync(PageTenantConnectionStringInput input)
        { 
            var result = new PagedResultDto<PageTenantConnectionStringOutput>();
            var tenant = await _tenantRepository.FindAsync(input.Id, true);

            if (tenant == null)
            {
                throw new BusinessException(BasicManagementErrorCodes.TenantNotExist);
            }

            result.TotalCount = tenant.ConnectionStrings.Count;

            var items = ObjectMapper.Map<List<TenantConnectionString>, List<PageTenantConnectionStringOutput>>(tenant.ConnectionStrings);
            if (input.Name.IsNotNullOrWhiteSpace())
            {
                items = items.Where(e => e.Name.ToLower().Contains(input.Name.ToLower())).ToList();
            }
            if (input.Value.IsNotNullOrWhiteSpace())
            {
                items = items.Where(e => e.Value.ToLower().Contains(input.Value.ToLower())).ToList();
            }
            result.Items = items;
          
            return result;
        }

        [Authorize(TenantManagementPermissions.Tenants.ManageConnectionStrings)]
        public async Task AddOrUpdateConnectionStringAsync(AddOrUpdateConnectionStringInput input)
        {
         
            // abp 租户，feature，background,setting 模块不支持单独配置数据库
            if (AbpTenantManagementDbProperties.ConnectionStringName.ToLower() == input.Name.ToLower() || 
                AbpBackgroundJobsDbProperties.ConnectionStringName.ToLower()== input.Name.ToLower() ||
                AbpFeatureManagementDbProperties.ConnectionStringName.ToLower() == input.Name.ToLower() ||
                AbpSettingManagementDbProperties.ConnectionStringName.ToLower() == input.Name.ToLower())
            {
                throw new BusinessException(BasicManagementErrorCodes.NotSupportSetConnectionString);
            }
            var tenant = await _tenantRepository.FindAsync(input.Id, true);

            if (tenant == null)
            {
                throw new BusinessException(BasicManagementErrorCodes.TenantNotExist);
            }

            var connectionString = tenant.ConnectionStrings.FirstOrDefault(e => e.Value == input.Name);
            if (connectionString == null)
            {
                tenant.SetConnectionString(input.Name, input.Value);
            }
            else
            {
                if (connectionString.Value != input.Value)
                {
                    tenant.SetConnectionString(input.Name, input.Value);
                }
            }
        }

        public async Task DeleteConnectionStringAsync(DeleteConnectionStringInput input)
        {
            
            var tenant = await _tenantRepository.FindAsync(input.TenantId, true);

            if (tenant == null)
            {
                throw new BusinessException(BasicManagementErrorCodes.TenantNotExist);
            }

            var connectionString = tenant.ConnectionStrings.FirstOrDefault(e => e.Name == input.Name);
            if (connectionString != null)
            {
                tenant.RemoveConnectionString(input.Name);
            }
        }
    }
}
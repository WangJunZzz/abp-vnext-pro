namespace Lion.AbpPro.BasicManagement.Tenants
{
    [Route("Tenants")]
    public class TenantController : BasicManagementController, IVoloTenantAppService
    {
        private readonly IVoloTenantAppService _voloTenantAppService;

        public TenantController(IVoloTenantAppService voloTenantAppService)
        {
            _voloTenantAppService = voloTenantAppService;
        }

        [HttpPost("find")]
        [SwaggerOperation(summary: "通过名称获取租户信息", Tags = new[] { "Tenants" })]
        public Task<FindTenantResultDto> FindTenantByNameAsync(FindTenantByNameInput input)
        {
            return _voloTenantAppService.FindTenantByNameAsync(input);
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取租户信息", Tags = new[] { "Tenants" })]
        public Task<PagedResultDto<TenantDto>> ListAsync(PagingTenantInput input)
        {
            return _voloTenantAppService.ListAsync(input);
        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "创建租户", Tags = new[] { "Tenants" })]
        public Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            return _voloTenantAppService.CreateAsync(input);
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "更新租户", Tags = new[] { "Tenants" })]
        public Task<TenantDto> UpdateAsync(UpdateTenantInput input)
        {
            var request = new TenantUpdateDto()
            {
                Name = input.Name.Trim()
            };
            return _voloTenantAppService.UpdateAsync(input);
        }

        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除租户", Tags = new[] { "Tenants" })]
        public Task DeleteAsync(IdInput input)
        {
            return _voloTenantAppService.DeleteAsync(input);
        }

        [HttpPost("getConnectionString")]
        [SwaggerOperation(summary: "获取租户连接字符串", Tags = new[] { "Tenants" })]
        public Task<string> GetDefaultConnectionStringAsync(IdInput input)
        {
            return _voloTenantAppService.GetDefaultConnectionStringAsync(input);
        }

        [HttpPost("updateConnectionString")]
        [SwaggerOperation(summary: "更新租户连接字符串", Tags = new[] { "Tenants" })]
        public Task UpdateDefaultConnectionStringAsync(UpdateConnectionStringInput input)
        {
            return _voloTenantAppService.UpdateDefaultConnectionStringAsync(input);
        }

        [HttpPost("deleteConnectionString")]
        [SwaggerOperation(summary: "删除租户连接字符串", Tags = new[] { "Tenants" })]
        public Task DeleteDefaultConnectionStringAsync(IdInput input)
        {
            return _voloTenantAppService.DeleteDefaultConnectionStringAsync(input);
        }
    }
}
namespace Lion.AbpPro.BasicManagement.Tenants
{
    public interface IVoloTenantAppService : IApplicationService
    {
        Task<FindTenantResultDto> FindTenantByNameAsync(FindTenantByNameInput input);

        Task<PagedResultDto<TenantDto>> ListAsync(PagingTenantInput input);

        Task<TenantDto> CreateAsync(TenantCreateDto input);

        Task<TenantDto> UpdateAsync(UpdateTenantInput input);

        Task DeleteAsync(IdInput input);

        /// <summary>
        /// 分页获取租户连接字符串
        /// </summary>
        Task<PagedResultDto<PageTenantConnectionStringOutput>> PageConnectionStringsAsync(PageTenantConnectionStringInput input);

        /// <summary>
        /// 新增或者更新连接字符串
        /// </summary>
        Task AddOrUpdateConnectionStringAsync(AddOrUpdateConnectionStringInput input);
        
        /// <summary>
        /// 删除连接字符串
        /// </summary>
        Task DeleteConnectionStringAsync(DeleteConnectionStringInput input);
    }
}
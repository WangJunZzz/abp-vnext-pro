namespace Lion.AbpPro.BasicManagement.Tenants
{
    public interface IVoloTenantAppService : IApplicationService
    {
        Task<FindTenantResultDto> FindTenantByNameAsync(FindTenantByNameInput input);

        Task<PagedResultDto<TenantDto>> ListAsync(PagingTenantInput input);

        Task<TenantDto> CreateAsync(TenantCreateDto input);

        Task<TenantDto> UpdateAsync(UpdateTenantInput input);

        Task DeleteAsync(IdInput input);

        Task<string> GetDefaultConnectionStringAsync(IdInput input);

        Task UpdateDefaultConnectionStringAsync(UpdateConnectionStringInput input);

        Task DeleteDefaultConnectionStringAsync(IdInput input);
    }
}
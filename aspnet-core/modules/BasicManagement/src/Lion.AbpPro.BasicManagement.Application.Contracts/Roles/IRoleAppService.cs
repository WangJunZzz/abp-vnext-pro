using Lion.AbpPro.BasicManagement.Roles.Dtos;
using Volo.Abp.Identity;

namespace Lion.AbpPro.BasicManagement.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task<ListResultDto<IdentityRoleDto>> AllListAsync();

        Task<PagedResultDto<IdentityRoleDto>> ListAsync(PagingRoleListInput input);

        Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input);

        Task<IdentityRoleDto> UpdateAsync(UpdateRoleInput input);

        Task DeleteAsync(IdInput input);
    }
}
using Lion.AbpPro.BasicManagement.Roles.Dtos;

namespace Lion.AbpPro.BasicManagement.Roles
{
    public interface IRolePermissionAppService : IApplicationService
    {
        
        Task<PermissionOutput> GetPermissionAsync(GetPermissionInput input);

        Task UpdatePermissionAsync(UpdateRolePermissionsInput input);
    }
}
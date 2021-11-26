using System.Threading.Tasks;
using Lion.AbpPro.Roles.Dtos;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.Roles
{
    public interface IRolePermissionAppService : IApplicationService
    {
        
        Task<PermissionOutput> GetPermissionAsync(GetPermissionInput input);

        Task UpdatePermissionAsync(UpdateRolePermissionsInput input);
    }
}
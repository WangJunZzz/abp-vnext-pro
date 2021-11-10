using System.Threading.Tasks;
using CompanyName.ProjectName.Roles.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.Roles
{
    public interface IRolePermissionAppService : IApplicationService
    {
        
        Task<PermissionOutput> GetPermissionAsync(GetPermissionInput input);

        Task UpdatePermissionAsync(UpdateRolePermissionsInput input);
    }
}
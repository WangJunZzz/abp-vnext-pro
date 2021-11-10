using System.Threading.Tasks;
using CompanyName.ProjectName.Roles;
using CompanyName.ProjectName.Roles.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Identity;

namespace CompanyName.ProjectName.Controllers.Systems
{
    [Route("Permissions")]
    [Authorize]
    public class PermissionController : ProjectNameController,IRolePermissionAppService
    {
        private readonly IRolePermissionAppService _rolePermissionAppService;

        public PermissionController(IRolePermissionAppService rolePermissionAppService)
        {
            _rolePermissionAppService = rolePermissionAppService;
        }


        [HttpPost("tree")]
        [SwaggerOperation(summary: "获取角色权限", Tags = new[] { "Permissions" })]
        public Task<PermissionOutput> GetPermissionAsync(GetPermissionInput input)
        {
            return _rolePermissionAppService.GetPermissionAsync(input);
        }

        [HttpPost("update")]
        [Authorize(IdentityPermissions.Roles.ManagePermissions)]
        [SwaggerOperation(summary: "更新角色", Tags = new[] { "Permissions" })]
        public Task UpdatePermissionAsync(UpdateRolePermissionsInput input)
        {
            return _rolePermissionAppService.UpdatePermissionAsync(input);
        }
    }
}
namespace Lion.AbpPro.BasicManagement.Systems
{
    [Route("Permissions")]
    public class PermissionController : BasicManagementController,IRolePermissionAppService
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
        [SwaggerOperation(summary: "更新角色", Tags = new[] { "Permissions" })]
        public Task UpdatePermissionAsync(UpdateRolePermissionsInput input)
        {
            return _rolePermissionAppService.UpdatePermissionAsync(input);
        }
    }
}
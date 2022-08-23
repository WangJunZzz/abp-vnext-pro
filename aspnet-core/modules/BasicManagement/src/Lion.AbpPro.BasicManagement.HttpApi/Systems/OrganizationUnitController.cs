namespace Lion.AbpPro.BasicManagement.Systems;

[Route("OrganizationUnits")]
public class OrganizationUnitController : BasicManagementController, IOrganizationUnitAppService
{
    private readonly IOrganizationUnitAppService _organizationUnitAppService;

    public OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService)
    {
        _organizationUnitAppService = organizationUnitAppService;
    }

    [HttpPost("tree")]
    [SwaggerOperation(summary: "获取组织机构树", Tags = new[] { "OrganizationUnits" })]
    public Task<List<TreeOutput>> GetTreeAsync()
    {
        return _organizationUnitAppService.GetTreeAsync();
    }

    [HttpPost("create")]
    [SwaggerOperation(summary: "创建组织机构", Tags = new[] { "OrganizationUnits" })]
    public Task CreateAsync(CreateOrganizationUnitInput input)
    {
        return _organizationUnitAppService.CreateAsync(input);
    }

    [HttpPost("delete")]
    [SwaggerOperation(summary: "删除组织机构", Tags = new[] { "OrganizationUnits" })]
    public Task DeleteAsync(IdInput input)
    {
        return _organizationUnitAppService.DeleteAsync(input);
    }

    [HttpPost("update")]
    [SwaggerOperation(summary: "编辑组织机构", Tags = new[] { "OrganizationUnits" })]
    public Task UpdateAsync(UpdateOrganizationUnitInput input)
    {
        return _organizationUnitAppService.UpdateAsync(input);
    }

    [HttpPost("addRoleToOrganizationUnitAsync")]
    [SwaggerOperation(summary: "向组织机构添加角色", Tags = new[] { "OrganizationUnits" })]
    public Task AddRoleToOrganizationUnitAsync(AddRoleToOrganizationUnitInput input)
    {
        return _organizationUnitAppService.AddRoleToOrganizationUnitAsync(input);
    }

    [HttpPost("removeRoleFromOrganizationUnitAsync")]
    [SwaggerOperation(summary: "向组织机构删除角色", Tags = new[] { "OrganizationUnits" })]
    public Task RemoveRoleFromOrganizationUnitAsync(RemoveRoleToOrganizationUnitInput input)
    {
        return _organizationUnitAppService.RemoveRoleFromOrganizationUnitAsync(input);
    }

    [HttpPost("addUserToOrganizationUnit")]
    [SwaggerOperation(summary: "向组织机构添加用户", Tags = new[] { "OrganizationUnits" })]
    public Task AddUserToOrganizationUnitAsync(AddUserToOrganizationUnitInput input)
    {
        return _organizationUnitAppService.AddUserToOrganizationUnitAsync(input);
    }

    [HttpPost("removeUserFromOrganizationUnit")]
    [SwaggerOperation(summary: "向组织机构删除用户", Tags = new[] { "OrganizationUnits" })]
    public Task RemoveUserFromOrganizationUnitAsync(RemoveUserToOrganizationUnitInput input)
    {
        return _organizationUnitAppService.RemoveUserFromOrganizationUnitAsync(input);
    }

    [HttpPost("getUsers")]
    [SwaggerOperation(summary: "分页获取组织机构下用户", Tags = new[] { "OrganizationUnits" })]
    public Task<PagedResultDto<GetOrganizationUnitUserOutput>> GetUsersAsync(GetOrganizationUnitUserInput input)
    {
        return _organizationUnitAppService.GetUsersAsync(input);
    }

    [HttpPost("getRoles")]
    [SwaggerOperation(summary: "分页获取组织机构下角色", Tags = new[] { "OrganizationUnits" })]
    public Task<PagedResultDto<GetOrganizationUnitRoleOutput>> GetRolesAsync(GetOrganizationUnitRoleInput input)
    {
        return _organizationUnitAppService.GetRolesAsync(input);
    }

    [HttpPost("getUnAddUsers")]
    [SwaggerOperation(summary: "获取不在组织机构的用户", Tags = new[] { "OrganizationUnits" })]
    public Task<PagedResultDto<GetUnAddUserOutput>> GetUnAddUsersAsync(GetUnAddUserInput input)
    {
        return _organizationUnitAppService.GetUnAddUsersAsync(input);
    }

    [HttpPost("getUnAddRoles")]
    [SwaggerOperation(summary: "获取不在组织机构的角色", Tags = new[] { "OrganizationUnits" })]
    public Task<PagedResultDto<GetUnAddRoleOutput>> GetUnAddRolessAsync(GetUnAddRoleInput input)
    {
        return _organizationUnitAppService.GetUnAddRolessAsync(input);
    }
}
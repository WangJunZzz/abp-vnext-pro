namespace Lion.AbpPro.BasicManagement.OrganizationUnits;

public interface IOrganizationUnitAppService : IApplicationService
{
    /// <summary>
    /// 获取组织机构树结构
    /// </summary>
    /// <returns></returns>
    Task<List<TreeOutput>> GetTreeAsync();

    /// <summary>
    /// 创建组织机构
    /// </summary>
    Task CreateAsync(CreateOrganizationUnitInput input);

    /// <summary>
    /// 删除组织机构
    /// </summary>
    Task DeleteAsync(IdInput input);

    /// <summary>
    /// 编辑组织机构
    /// </summary>
    Task UpdateAsync(UpdateOrganizationUnitInput input);

    /// <summary>
    /// 向组织机构添加角色
    /// </summary>
    Task AddRoleToOrganizationUnitAsync(AddRoleToOrganizationUnitInput input);

    /// <summary>
    /// 向组织机构删除角色
    /// </summary>
    Task RemoveRoleFromOrganizationUnitAsync(RemoveRoleToOrganizationUnitInput input);

    /// <summary>
    /// 向组织机构添加用户
    /// </summary>
    Task AddUserToOrganizationUnitAsync(AddUserToOrganizationUnitInput input);

    /// <summary>
    /// 向组织机构删除用户
    /// </summary>
    Task RemoveUserFromOrganizationUnitAsync(RemoveUserToOrganizationUnitInput input);
    
    /// <summary>
    /// 分页获取组织机构下用户
    /// </summary>
    Task<PagedResultDto<GetOrganizationUnitUserOutput>> GetUsersAsync(GetOrganizationUnitUserInput input);
    

    /// <summary>
    /// 分页获取组织机构下角色
    /// </summary>
    Task<PagedResultDto<GetOrganizationUnitRoleOutput>> GetRolesAsync(GetOrganizationUnitRoleInput input);

    /// <summary>
    /// 获取不在组织机构的用户
    /// </summary>
    Task<PagedResultDto<GetUnAddUserOutput>> GetUnAddUsersAsync(GetUnAddUserInput input);

    /// <summary>
    /// 获取不在组织机构的角色
    /// </summary>
    Task<PagedResultDto<GetUnAddRoleOutput>> GetUnAddRolessAsync(GetUnAddRoleInput input);
}
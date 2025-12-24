using Mapster;

namespace Lion.AbpPro.BasicManagement.Roles;

[Authorize(Policy = IdentityPermissions.Roles.Default)]
public class RoleAppService : BasicManagementAppService, IRoleAppService
{
    private readonly IIdentityRoleAppService _identityRoleAppService;
    private readonly IIdentityRoleRepository _roleRepository;
    private readonly ICurrentTenant _currentTenant;

    public RoleAppService(
        IIdentityRoleAppService identityRoleAppService,
        IIdentityRoleRepository roleRepository,
        ICurrentTenant currentTenant)
    {
        _identityRoleAppService = identityRoleAppService;

        _roleRepository = roleRepository;
        _currentTenant = currentTenant;
    }

    /// <summary>
    /// 获取所有角色
    /// </summary>
      
    public virtual async Task<ListResultDto<IdentityRoleDto>> AllListAsync()
    {
        List<IdentityRole> source =
            await _roleRepository.GetListAsync()
                .ConfigureAwait(continueOnCapturedContext: false);
        return new ListResultDto<IdentityRoleDto>(source.Adapt<List<IdentityRoleDto>>());
    }

    /// <summary>
    /// 分页查询角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public virtual async Task<PagedResultDto<IdentityRoleDto>> ListAsync(PagingRoleListInput input)
    {
        var request = new GetIdentityRolesInput
        {
            Filter = input.Filter?.Trim(), MaxResultCount = input.PageSize,
            SkipCount = input.SkipCount
        };
        List<IdentityRole> list = await _roleRepository
            .GetListAsync(request.Sorting, request.MaxResultCount, request.SkipCount,
                request.Filter)
            .ConfigureAwait(continueOnCapturedContext: false);
        return new PagedResultDto<IdentityRoleDto>(
            await _roleRepository.GetCountAsync(request.Filter)
                .ConfigureAwait(continueOnCapturedContext: false),
            list.Adapt<List<IdentityRoleDto>>());
    }


    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Roles.Create)]
    public virtual async Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
    {
        var s = _currentTenant;
        return await _identityRoleAppService.CreateAsync(input);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    [Authorize(IdentityPermissions.Roles.Update)]
    public virtual async Task<IdentityRoleDto> UpdateAsync(UpdateRoleInput input)
    {
        return await _identityRoleAppService.UpdateAsync(input.RoleId, input.RoleInfo);
    }


    /// <summary>
    /// 删除角色
    /// </summary>
    [Authorize(IdentityPermissions.Roles.Delete)]
    public virtual async Task DeleteAsync(IdInput input)
    {
        await _identityRoleAppService.DeleteAsync(input.Id);
    }
}
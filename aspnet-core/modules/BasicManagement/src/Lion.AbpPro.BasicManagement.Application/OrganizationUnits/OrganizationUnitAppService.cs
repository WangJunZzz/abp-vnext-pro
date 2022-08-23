namespace Lion.AbpPro.BasicManagement.OrganizationUnits;

[Authorize]
public class OrganizationUnitAppService : BasicManagementAppService, IOrganizationUnitAppService
{
    private readonly OrganizationUnitManager _organizationUnitManager;
    private readonly IdentityUserManager _identityUserManager;
    private readonly IOrganizationUnitRepository _organizationUnitRepository;

    public OrganizationUnitAppService(
        OrganizationUnitManager OrganizationUnitManager,
        IdentityUserManager identityUserManager,
        IOrganizationUnitRepository organizationUnitRepository)
    {
        _organizationUnitManager = OrganizationUnitManager;
        _identityUserManager = identityUserManager;
        _organizationUnitRepository = organizationUnitRepository;
    }

    public async Task<List<TreeOutput>> GetTreeAsync()
    {
        var organizationUnits = await _organizationUnitRepository.GetListAsync();
        var organizationUnitDtos = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(organizationUnits);
        return ConvertToTree(organizationUnitDtos);
    }

    [Authorize(BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Create)]
    public async Task CreateAsync(CreateOrganizationUnitInput input)
    {
        var entity = new OrganizationUnit
        (
            GuidGenerator.Create(),
            input.DisplayName,
            input.ParentId,
            CurrentTenant.Id
        );
        await _organizationUnitManager.CreateAsync(entity);
    }

    [Authorize(BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Delete)]
    public Task DeleteAsync(IdInput input)
    {
        return _organizationUnitManager.DeleteAsync(input.Id);
    }

    [Authorize(BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Update)]
    public async Task UpdateAsync(UpdateOrganizationUnitInput input)
    {
        var entity = await _organizationUnitRepository.FindAsync(input.Id);
        if (entity != null)
        {
            entity.DisplayName = input.DisplayName;
            await _organizationUnitManager.UpdateAsync(entity);
        }
    }

    [Authorize(BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Create)]
    public async Task AddRoleToOrganizationUnitAsync(AddRoleToOrganizationUnitInput input)
    {
        foreach (var roleId in input.RoleId)
        {
            await _organizationUnitManager.AddRoleToOrganizationUnitAsync(roleId, input.OrganizationUnitId);
        }
    }

    [Authorize(BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Delete)]
    public async Task RemoveRoleFromOrganizationUnitAsync(RemoveRoleToOrganizationUnitInput input)
    {
        await _organizationUnitManager.RemoveRoleFromOrganizationUnitAsync(input.RoleId, input.OrganizationUnitId);
    }

    [Authorize(BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Create)]
    public async Task AddUserToOrganizationUnitAsync(AddUserToOrganizationUnitInput input)
    {
        foreach (var userId in input.UserId)
        {
            await _identityUserManager.AddToOrganizationUnitAsync(userId, input.OrganizationUnitId);
        }
    }

    [Authorize(BasicManagementPermissions.SystemManagement.OrganizationUnitManagement.Delete)]
    public async Task RemoveUserFromOrganizationUnitAsync(RemoveUserToOrganizationUnitInput input)
    {
        await _identityUserManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
    }

    public async Task<PagedResultDto<GetOrganizationUnitUserOutput>> GetUsersAsync(GetOrganizationUnitUserInput input)
    {
        var listResult = new List<GetOrganizationUnitUserOutput>();
        var organizationUnit = await _organizationUnitRepository.FindAsync(input.OrganizationUnitId);
        if (organizationUnit == null) throw new BusinessException(BasicManagementErrorCodes.OrganizationUnitNotExist);

        var count = await _organizationUnitRepository.GetMembersCountAsync(organizationUnit, filter: input.Filter);
        if (count > 0)
        {
            var list = await _organizationUnitRepository.GetMembersAsync
            (
                organizationUnit,
                maxResultCount: input.PageSize,
                skipCount: input.SkipCount,
                filter: input.Filter
            );
            listResult = ObjectMapper.Map<List<IdentityUser>, List<GetOrganizationUnitUserOutput>>(list);
        }

        return new PagedResultDto<GetOrganizationUnitUserOutput>(count, listResult);
    }

    public async Task<PagedResultDto<GetUnAddUserOutput>> GetUnAddUsersAsync(GetUnAddUserInput input)
    {
        var listResult = new List<GetUnAddUserOutput>();
        var organizationUnit = await _organizationUnitRepository.FindAsync(input.OrganizationUnitId);
        if (organizationUnit == null) throw new BusinessException(BasicManagementErrorCodes.OrganizationUnitNotExist);
        var count = await _organizationUnitRepository.GetUnaddedUsersCountAsync(organizationUnit, input.Filter);
        if (count > 0)
        {
            var users = await _organizationUnitRepository.GetUnaddedUsersAsync
            (
                organizationUnit,
                maxResultCount: input.PageSize,
                skipCount: input.SkipCount,
                filter: input.Filter
            );
            listResult = ObjectMapper.Map<List<IdentityUser>, List<GetUnAddUserOutput>>(users);
        }

        return new PagedResultDto<GetUnAddUserOutput>(count, listResult);
    }

    public async Task<PagedResultDto<GetOrganizationUnitRoleOutput>> GetRolesAsync(GetOrganizationUnitRoleInput input)
    {
        var listResult = new List<GetOrganizationUnitRoleOutput>();
        var organizationUnit = await _organizationUnitRepository.FindAsync(input.OrganizationUnitId);
        if (organizationUnit == null) throw new BusinessException(BasicManagementErrorCodes.OrganizationUnitNotExist);

        var count = await _organizationUnitRepository.GetRolesCountAsync(organizationUnit);
        if (count > 0)
        {
            var list = await _organizationUnitRepository.GetRolesAsync(organizationUnit, maxResultCount: input.PageSize, skipCount: input.SkipCount);
            listResult = ObjectMapper.Map<List<IdentityRole>, List<GetOrganizationUnitRoleOutput>>(list);
        }

        return new PagedResultDto<GetOrganizationUnitRoleOutput>(count, listResult);
    }

    public async Task<PagedResultDto<GetUnAddRoleOutput>> GetUnAddRolessAsync(GetUnAddRoleInput input)
    {
        var listResult = new List<GetUnAddRoleOutput>();
        var organizationUnit = await _organizationUnitRepository.FindAsync(input.OrganizationUnitId);
        if (organizationUnit == null) throw new BusinessException(BasicManagementErrorCodes.OrganizationUnitNotExist);
        var count = await _organizationUnitRepository.GetUnaddedRolesCountAsync(organizationUnit, input.Filter);
        if (count > 0)
        {
            var roles = await _organizationUnitRepository.GetUnaddedRolesAsync
            (
                organizationUnit,
                maxResultCount: input.PageSize,
                skipCount: input.SkipCount,
                filter: input.Filter
            );
            listResult = ObjectMapper.Map<List<IdentityRole>, List<GetUnAddRoleOutput>>(roles);
        }

        return new PagedResultDto<GetUnAddRoleOutput>(count, listResult);
    }

    #region 私有方法

    private List<TreeOutput> ConvertToTree(
        List<OrganizationUnitDto> list,
        Guid? Id = null)
    {
        var result = new List<TreeOutput>();
        var childList = Children(list, Id);
        foreach (var item in childList)
        {
            var tree = new TreeOutput
            {
                Key = item.Id,
                Title = item.DisplayName,
                Children = ConvertToTree(list, item.Id)
            };
            result.Add(tree);
        }

        return result;
    }

    private List<OrganizationUnitDto> Children(
        List<OrganizationUnitDto> list,
        Guid? Id)
    {
        var childList = list.Where(x => x.ParentId == Id).ToList();
        return childList;
    }

    #endregion
}
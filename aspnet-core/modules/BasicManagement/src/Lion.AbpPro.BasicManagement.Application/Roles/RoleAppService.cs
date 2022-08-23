using Lion.AbpPro.BasicManagement.Roles.Dtos;

namespace Lion.AbpPro.BasicManagement.Roles
{
    [Authorize]
    public class RoleAppService : BasicManagementAppService, IRoleAppService
    {
        private readonly IIdentityRoleAppService _identityRoleAppService;

        private readonly IIdentityRoleRepository _roleRepository;

        public RoleAppService(
            IIdentityRoleAppService identityRoleAppService,
            IIdentityRoleRepository roleRepository)
        {
            _identityRoleAppService = identityRoleAppService;

            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
      
        public async Task<ListResultDto<IdentityRoleDto>> AllListAsync()
        {
            List<IdentityRole> source =
                await _roleRepository.GetListAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);
            return new ListResultDto<IdentityRoleDto>(
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(source));
        }

        /// <summary>
        /// 分页查询角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<IdentityRoleDto>> ListAsync(PagingRoleListInput input)
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
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list));
        }


        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Roles.Create)]
        public async Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            return await _identityRoleAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        [Authorize(IdentityPermissions.Roles.Update)]
        public async Task<IdentityRoleDto> UpdateAsync(UpdateRoleInput input)
        {
            return await _identityRoleAppService.UpdateAsync(input.RoleId, input.RoleInfo);
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        [Authorize(IdentityPermissions.Roles.Delete)]
        public async Task DeleteAsync(IdInput input)
        {
            await _identityRoleAppService.DeleteAsync(input.Id);
        }
    }
}
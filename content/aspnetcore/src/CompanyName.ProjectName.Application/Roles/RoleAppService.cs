using CompanyNameProjectName.Roles.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace CompanyNameProjectName.Roles
{
    public class RoleAppService : ApplicationService
    {
        private readonly IIdentityRoleAppService _identityRoleAppService;
        private readonly IPermissionAppService _permissionAppService;
        protected IIdentityRoleRepository _roleRepository;
        public RoleAppService(IIdentityRoleAppService identityRoleAppService, IPermissionAppService permissionAppService, IIdentityRoleRepository roleRepository)
        {
            _identityRoleAppService = identityRoleAppService;
            _permissionAppService = permissionAppService;
            _roleRepository = roleRepository;
        }

        [HttpPost]
        [SwaggerOperation(summary: "获取所有角色", Tags = new[] { "Role" })]
        public async Task<ListResultDto<IdentityRoleDto>> AllListAsync()
        {
            List<IdentityRole> source = await _roleRepository.GetListAsync().ConfigureAwait(continueOnCapturedContext: false);
            return new ListResultDto<IdentityRoleDto>(base.ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(source));
            //return await _identityRoleAppService.GetAllListAsync();
        }

        [HttpPost]
        [SwaggerOperation(summary: "分页获取角色列表", Tags = new[] { "Role" })]
        public async Task<PagedResultDto<IdentityRoleDto>> ListAsync(GetRoleListInput input)
        {
            var request = new GetIdentityRolesInput();
            request.Filter = input.filter?.Trim();
            request.MaxResultCount = input.PageSize;
            request.SkipCount = (input.PageIndex - 1) * input.PageSize;
            return await _identityRoleAppService.GetListAsync(request);
        }

        [Authorize("AbpIdentity.Roles.Create")]
        [HttpPost]
        [SwaggerOperation(summary: "创建角色", Tags = new[] { "Role" })]
        public async Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            return await _identityRoleAppService.CreateAsync(input);
        }

        [Authorize("AbpIdentity.Roles.Update")]
        [HttpPost]
        [SwaggerOperation(summary: "更新角色", Tags = new[] { "Role" })]
        public async Task<IdentityRoleDto> UpdateAsync(UpdateRoleInput input)
        {
            return await _identityRoleAppService.UpdateAsync(input.RoleId, input.RoleInfo);
        }

        [Authorize("AbpIdentity.Roles.Delete")]
        [SwaggerOperation(summary: "删除角色", Tags = new[] { "Role" })]
        public async Task DeleteAsync(Guid id)
        {
            await _identityRoleAppService.DeleteAsync(id);
        }

        [SwaggerOperation(summary: "获取角色权限", Tags = new[] { "Role" })]
        public async Task<GetPermissionListResultDto> GetPermissionAsync(string providerName, string providerKey)
        {
            return await _permissionAppService.GetAsync(providerName, providerKey);
        }

        [SwaggerOperation(summary: "修改角色权限", Tags = new[] { "Role" })]
        [HttpPost]
        [Authorize("AbpIdentity.Roles.ManagePermissions")]
        public async Task UpdatePermissionAsync(UpdateRolePermissionsDto input)
        {
            await _permissionAppService.UpdateAsync(input.ProviderName, input.ProviderKey, input.UpdatePermissionsDto);
        }
    }
}

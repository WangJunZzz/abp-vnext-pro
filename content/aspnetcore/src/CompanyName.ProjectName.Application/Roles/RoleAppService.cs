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
    [Authorize]
    public class RoleAppService : ApplicationService
    {
        private readonly IIdentityRoleAppService _identityRoleAppService;
        private readonly IPermissionAppService _permissionAppService;
        private readonly IIdentityRoleRepository _roleRepository;

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
        [Authorize("AbpIdentity.Roles.Query")]
        public async Task<PagedResultDto<IdentityRoleDto>> ListAsync(GetRoleListInput input)
        {
            var request = new GetIdentityRolesInput();
            request.Filter = input.filter?.Trim();
            request.MaxResultCount = input.PageSize;
            request.SkipCount = (input.PageIndex - 1) * input.PageSize;
            List<IdentityRole> list = await _roleRepository.GetListAsync(request.Sorting, request.MaxResultCount, request.SkipCount, request.Filter).ConfigureAwait(continueOnCapturedContext: false);
            return new PagedResultDto<IdentityRoleDto>(await _roleRepository.GetCountAsync(request.Filter).ConfigureAwait(continueOnCapturedContext: false), base.ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list));

            //return await _identityRoleAppService.GetListAsync(request);
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
        public async Task<PermissionOutput> GetPermissionAsync(string providerName, string providerKey)
        {
            var permissions = await _permissionAppService.GetAsync(providerName, providerKey);
            return BuildTreeData(permissions.Groups);
        }

        [SwaggerOperation(summary: "修改角色权限", Tags = new[] { "Role" })]
        [HttpPost]
        [Authorize("AbpIdentity.Roles.ManagePermissions")]
        public async Task UpdatePermissionAsync(UpdateRolePermissionsDto input)
        {
            await _permissionAppService.UpdateAsync(input.ProviderName, input.ProviderKey, input.UpdatePermissionsDto);
        }

        private PermissionOutput BuildTreeData(List<PermissionGroupDto> input)
        {
            var result = new PermissionOutput();
            var excludes = new List<string> {
                "AbpIdentity.Users.ManagePermissions",
                "FeatureManagement",
                "FeatureManagement.ManageHostFeatures",
                "AbpTenantManagement",
                "AbpTenantManagement.Tenants",
                "AbpTenantManagement.Tenants.Create",
                "AbpTenantManagement.Tenants.Update",
                "AbpTenantManagement.Tenants.Delete",
                "AbpTenantManagement.Tenants.ManageFeatures",
                "AbpTenantManagement.Tenants.ManageConnectionStrings"
            };

            var permissions = new List<PermissionTreeDto>();

            foreach (var group in input)
            {
                if (excludes.Any(e => e == group.Name)) continue;
               
                var groupPermission = new PermissionTreeDto();
                groupPermission.Key = group.Name;
                groupPermission.Title = group.DisplayName;
                foreach (var item in group.Permissions)
                {
                    result.AllGrants.Add(item.Name);
                    if (item.IsGranted)
                    {
                        result.Grants.Add(item.Name);
                    }
                    //获取ParentName=null的权限
                    var management = group.Permissions.Where(e => e.ParentName.IsNullOrWhiteSpace()).ToList();

                    foreach (var managementItem in management)
                    {
                        if (!groupPermission.Children.Any(e => e.Key == managementItem.Name))
                        {
                            var managementPermission = new PermissionTreeDto()
                            {
                                Key = managementItem.Name,
                                Title = managementItem.DisplayName
                            };
                            // 获取management下权限
                            var childrenPermission = group.Permissions.Where(e => e.ParentName==managementItem.Name).ToList();
                            foreach (var childrenPermissionItem in childrenPermission)
                            {
                                managementPermission.Children.Add(new PermissionTreeDto()
                                {
                                    Key = childrenPermissionItem.Name,
                                    Title = childrenPermissionItem.DisplayName
                                });
                            }
                            groupPermission.Children.Add(managementPermission);
                        }
                    }
                }
                permissions.Add(groupPermission);
            }
            result.Permissions = permissions;
            return result;
        }

    }
}

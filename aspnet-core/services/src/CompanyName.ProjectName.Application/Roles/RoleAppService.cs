using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.Roles.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace CompanyName.ProjectName.Roles
{
    public class RoleAppService : ProjectNameAppService, IRoleAppService
    {
        private readonly IIdentityRoleAppService _identityRoleAppService;
        private readonly IPermissionAppService _permissionAppService;
        private readonly IIdentityRoleRepository _roleRepository;

        public RoleAppService(IIdentityRoleAppService identityRoleAppService,
            IPermissionAppService permissionAppService, IIdentityRoleRepository roleRepository)
        {
            _identityRoleAppService = identityRoleAppService;
            _permissionAppService = permissionAppService;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<IdentityRoleDto>> AllListAsync()
        {
            List<IdentityRole> source =
                await _roleRepository.GetListAsync().ConfigureAwait(continueOnCapturedContext: false);
            return new ListResultDto<IdentityRoleDto>(
                base.ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(source));
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
                Filter = input.Filter?.Trim(), MaxResultCount = input.PageSize, SkipCount = input.SkipCount
            };
            List<IdentityRole> list = await _roleRepository
                .GetListAsync(request.Sorting, request.MaxResultCount, request.SkipCount, request.Filter)
                .ConfigureAwait(continueOnCapturedContext: false);
            return new PagedResultDto<IdentityRoleDto>(
                await _roleRepository.GetCountAsync(request.Filter).ConfigureAwait(continueOnCapturedContext: false),
                base.ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list));
        }


        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            return await _identityRoleAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IdentityRoleDto> UpdateAsync(UpdateRoleInput input)
        {
            return await _identityRoleAppService.UpdateAsync(input.RoleId, input.RoleInfo);
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteAsync(Guid id)
        {
            await _identityRoleAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public async Task<PermissionOutput> GetPermissionAsync(GetPermissionInput input)
        {
            var permissions = await _permissionAppService.GetAsync(input.ProviderName, input.ProviderKey);
            return BuildTreeData(permissions.Groups);
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="input"></param>
        public async Task UpdatePermissionAsync(UpdateRolePermissionsInput input)
        {
            await _permissionAppService.UpdateAsync(input.ProviderName, input.ProviderKey, input.UpdatePermissionsDto);
        }

        /// <summary>
        /// 生成权限树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private PermissionOutput BuildTreeData(List<PermissionGroupDto> input)
        {
            var result = new PermissionOutput();
            var excludes = new List<string>
            {
                "AbpIdentity.Users.ManagePermissions",
                "FeatureManagement",
                "FeatureManagement.ManageHostFeatures",
                "AbpTenantManagement",
                "AbpTenantManagement.Tenants",
                "AbpTenantManagement.Tenants.Create",
                "AbpTenantManagement.Tenants.Update",
                "AbpTenantManagement.Tenants.Delete",
                "AbpTenantManagement.Tenants.ManageFeatures",
                "AbpTenantManagement.Tenants.ManageConnectionStrings",
                "SettingManagement",
                "SettingManagement.Emailing",
                "SettingUi"
            };

            var permissions = new List<PermissionTreeDto>();

            foreach (var group in input)
            {
                if (excludes.Any(e => e == group.Name))
                {
                    continue;
                }

                var groupPermission = new PermissionTreeDto {Key = @group.Name, Title = @group.DisplayName};
                groupPermission.Key = group.Name;
                groupPermission.Title =
                    group.Name == "AbpIdentity" ? L["Permission:SystemManagement"] : group.DisplayName;
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
                        if (groupPermission.Children.Any(e => e.Key == managementItem.Name))
                        {
                            continue;
                        }

                        {
                            var managementPermission = new PermissionTreeDto()
                            {
                                Key = managementItem.Name,
                                Title = managementItem.DisplayName
                            };
                            // 获取management下权限
                            var childrenPermission = @group.Permissions.Where(e => e.ParentName == managementItem.Name)
                                .ToList();
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

            var settingUIPermisstion = "SettingUi.ShowSettingPage";
            // https://github.com/EasyAbp/Abp.SettingUi/blob/develop/src/EasyAbp.Abp.SettingUi.Application/SettingUiAppService.cs
            // 因为使用的SettingUI模块，想把权限添加到系统管理下 
            result.Permissions.First(e => e.Key == "AbpIdentity").Children.Add(new PermissionTreeDto()
            {
                Key = settingUIPermisstion,
                Title = L["Permission:SettingUi:ShowSettingPage"]
            });
            result.AllGrants.Add(settingUIPermisstion);

            var setting = input.First(e => e.Name == "SettingUi").Permissions.First(e => e.Name == settingUIPermisstion);
            if (setting.IsGranted)
            {
                result.Grants.Add(settingUIPermisstion);
            }
            return result;
        }
    }
}
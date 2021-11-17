using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.Roles.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace CompanyName.ProjectName.Roles
{
    [Authorize]
    public class RolePermissionAppService : ProjectNameAppService, IRolePermissionAppService
    {
        private readonly IPermissionAppService _rolePermissionAppService;

        public RolePermissionAppService(IPermissionAppService rolePermissionAppService)
        {
            _rolePermissionAppService = rolePermissionAppService;
        }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public async Task<PermissionOutput> GetPermissionAsync(GetPermissionInput input)
        {
            var permissions =
                await _rolePermissionAppService.GetAsync(input.ProviderName, input.ProviderKey);
            return BuildTreeData(permissions.Groups);
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="input"></param>
        [Authorize(IdentityPermissions.Roles.ManagePermissions)]
        public async Task UpdatePermissionAsync(UpdateRolePermissionsInput input)
        {
            await _rolePermissionAppService.UpdateAsync(input.ProviderName, input.ProviderKey,
                input.UpdatePermissionsDto);
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
                // "AbpTenantManagement",
                // "AbpTenantManagement.Tenants",
                // "AbpTenantManagement.Tenants.Create",
                // "AbpTenantManagement.Tenants.Update",
                // "AbpTenantManagement.Tenants.Delete",
                "AbpTenantManagement.Tenants.ManageFeatures",
                // "AbpTenantManagement.Tenants.ManageConnectionStrings",
                "SettingManagement",
                "SettingManagement.Emailing",
                "SettingUi"
            };

            var permissions = new List<PermissionTreeDto>();

            foreach (var group in input)
            {
                if (excludes.Any(e => e == group.Name)) continue;

                // 获取分组信息
                var groupPermission = new PermissionTreeDto
                {
                    Key = @group.Name,
                    Title = @group.Name == "AbpIdentity"
                        ? L["Permission:SystemManagement"]
                        : @group.DisplayName
                };

                // 获取所有已授权和未授权权限集合
                foreach (var item in group.Permissions)
                {
                    result.AllGrants.Add(item.Name);
                    if (item.IsGranted)
                    {
                        result.Grants.Add(item.Name);
                    }
                }
                
                // 递归菜单
                var childTreeMenu = RecursionMenu(group.Permissions, null);

                groupPermission.Children.AddRange(childTreeMenu.Children);
                
                permissions.Add(groupPermission);
            }

            result.Permissions = permissions;

            var settingUIPermisstion = "SettingUi.ShowSettingPage";
            // https://github.com/EasyAbp/Abp.SettingUi/blob/develop/src/EasyAbp.Abp.SettingUi.Application/SettingUiAppService.cs
            // 因为使用的SettingUI模块，想把权限添加到系统管理下 
            result.Permissions.First(e => e.Key == "AbpIdentity").Children.Add(
                new PermissionTreeDto()
                {
                    Key = settingUIPermisstion,
                    Title = L["Permission:SettingUi:ShowSettingPage"]
                });
            result.AllGrants.Add(settingUIPermisstion);

            var setting = input.First(e => e.Name == "SettingUi").Permissions
                .First(e => e.Name == settingUIPermisstion);
            if (setting.IsGranted)
            {
                result.Grants.Add(settingUIPermisstion);
            }

            return result;
        }
        
        
        /// <summary>
        /// 递归菜单
        /// </summary>
        private PermissionTreeDto RecursionMenu(List<PermissionGrantInfoDto> permissionGrantInfoDtos,
            string parentName)
        {
            var tree = new PermissionTreeDto();
            var permissions = permissionGrantInfoDtos
                .Where(e => e.ParentName == parentName).ToList();
            foreach (var item in permissions)
            {
                var child = new PermissionTreeDto
                {
                    Key = item.Name,
                    Title = item.DisplayName
                };
                child.Children.AddRange(RecursionMenu(permissionGrantInfoDtos, item.Name).Children);
                tree.Children.Add(child);
            }
            return tree;
        }
    }
}
using System;
using System.Threading.Tasks;
using Lion.AbpPro.Roles;
using Lion.AbpPro.Roles.Dtos;
using Lion.AbpPro.Extension.Customs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Lion.AbpPro.Controllers.Systems
{
    [Route("Roles")]
    public class RoleController : AbpProController, IRoleAppService
    {
        private readonly IRoleAppService _roleAppService;

        public RoleController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        [HttpPost("all")]
        [SwaggerOperation(summary: "获取所有角色", Tags = new[] { "Roles" })]
        public Task<ListResultDto<IdentityRoleDto>> AllListAsync()
        {
            return _roleAppService.AllListAsync();
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取角色", Tags = new[] { "Roles" })]
        public Task<PagedResultDto<IdentityRoleDto>> ListAsync(PagingRoleListInput input)
        {
            return _roleAppService.ListAsync(input);
        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "创建角色", Tags = new[] { "Roles" })]
        public Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            return _roleAppService.CreateAsync(input);
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "更新角色", Tags = new[] { "Roles" })]
        public Task<IdentityRoleDto> UpdateAsync(UpdateRoleInput input)
        {
            return _roleAppService.UpdateAsync(input);
        }


        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除角色", Tags = new[] { "Roles" })]
        public Task DeleteAsync(IdInput input)
        {
            return _roleAppService.DeleteAsync(input);
        }
    }
}
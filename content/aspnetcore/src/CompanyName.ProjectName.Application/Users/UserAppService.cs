using CompanyNameProjectName.Users.Dtos;
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

namespace CompanyNameProjectName.Users
{
    public class UserAppService : ApplicationService
    {
        private readonly IIdentityUserAppService _identityUserAppService;

        public UserAppService(IIdentityUserAppService identityUserAppService)
        {
            _identityUserAppService = identityUserAppService;
        }

        [SwaggerOperation(summary: "分页获取用户信息", Tags = new[] { "User" })]
        [Authorize("AbpIdentity.Users")]
        [HttpPost]
        public async Task<PagedResultDto<IdentityUserDto>> ListAsync(GetUserListInput input)
        {
            var request = new GetIdentityUsersInput();
            request.Filter = input.filter?.Trim();
            request.MaxResultCount = input.PageSize;
            request.SkipCount = (input.PageIndex - 1) * input.PageSize;
            request.Sorting= " LastModificationTime desc";
            return await _identityUserAppService.GetListAsync(request);
        }

        [SwaggerOperation(summary: "创建用户", Tags = new[] { "User" })]
        [Authorize("AbpIdentity.Users.Create")]
        [HttpPost]
        public  async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            return await _identityUserAppService.CreateAsync(input);
        }

        [SwaggerOperation(summary: "更新用户", Tags = new[] { "User" })]
        [Authorize("AbpIdentity.Users.Update")]
        [HttpPost]
        public virtual async Task<IdentityUserDto> UpdateAsync(UpdateUserInput input)
        {
            return await _identityUserAppService.UpdateAsync(input.UserId,input.UserInfo);
        }

        [SwaggerOperation(summary: "删除用户", Tags = new[] { "User" })]
        [Authorize("AbpIdentity.Users.Delete")]
        public virtual async Task DeleteAsync(Guid id)
        {
             await _identityUserAppService.DeleteAsync(id);
        }

        [SwaggerOperation(summary: "获取用户角色", Tags = new[] { "User" })]
        [Authorize("AbpIdentity.Users")]
        [HttpPost("/api/app/user/role/{userId}")]
        public  async Task<ListResultDto<IdentityRoleDto>> GetRoleByUserId(Guid userId)
        {
            return await _identityUserAppService.GetRolesAsync(userId);
        }
    }
}

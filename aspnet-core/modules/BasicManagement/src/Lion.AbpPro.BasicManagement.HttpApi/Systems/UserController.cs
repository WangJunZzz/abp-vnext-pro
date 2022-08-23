using System.Net;
using Volo.Abp.Account;

namespace Lion.AbpPro.BasicManagement.Systems
{
    [Route("Users")]
    public class UserController : BasicManagementController, IUserAppService
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取用户信息", Tags = new[] { "Users" })]
        public Task<PagedResultDto<IdentityUserDto>> ListAsync(PagingUserListInput input)
        {
            return _userAppService.ListAsync(input);
        }
        [HttpPost("export")]
        [SwaggerOperation(summary: "导出用户列表", Tags = new[] { "Users" })]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        public Task<ActionResult> ExportAsync(PagingUserListInput input)
        {
            return _userAppService.ExportAsync(input);
        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "创建用户", Tags = new[] { "Users" })]
        public Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            return _userAppService.CreateAsync(input);
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "编辑用户", Tags = new[] { "Users" })]
        public Task<IdentityUserDto> UpdateAsync(UpdateUserInput input)
        {
            return _userAppService.UpdateAsync(input);
        }

        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除用户", Tags = new[] { "Users" })]
        public Task DeleteAsync(IdInput input)
        {
            return _userAppService.DeleteAsync(input);
        }


        [HttpPost("role")]
        [SwaggerOperation(summary: "获取用户角色信息", Tags = new[] { "Users" })]
        public Task<ListResultDto<IdentityRoleDto>> GetRoleByUserId(IdInput input)
        {
            return _userAppService.GetRoleByUserId(input);
        }

        [HttpPost("changePassword")]
        [SwaggerOperation(summary: "修改当前用户密码", Tags = new[] { "Users" })]
        public Task<bool> ChangePasswordAsync(ChangePasswordInput input)
        {
            return _userAppService.ChangePasswordAsync(input);
        }

        [HttpPost("lock")]
        [SwaggerOperation(summary: "锁定用户", Tags = new[] { "Users" })]
        public Task LockAsync(LockUserInput input)
        {
            return _userAppService.LockAsync(input);
        }
    }
}
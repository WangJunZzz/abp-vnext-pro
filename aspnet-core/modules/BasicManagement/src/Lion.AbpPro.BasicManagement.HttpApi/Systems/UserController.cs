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
        public Task<PagedResultDto<PageIdentityUserOutput>> ListAsync(PagingUserListInput input)
        {
            return _userAppService.ListAsync(input);
        }

        [HttpPost("list")]
        [SwaggerOperation(summary: "分页获取用户信息", Tags = new[] { "Users" })]
        public Task<List<IdentityUserDto>> ListAllAsync(PagingUserListInput input)
        {
            return _userAppService.ListAllAsync(input);
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

        [HttpPost("resetPassword")]
        [SwaggerOperation(summary: "重置密码", Tags = new[] { "Users" })]
        public Task<bool> RestPasswordAsync(ResetPasswordInput input)
        {
            return _userAppService.RestPasswordAsync(input);
        }

        [HttpPost("lock")]
        [SwaggerOperation(summary: "锁定用户", Tags = new[] { "Users" })]
        public Task LockAsync(LockUserInput input)
        {
            return _userAppService.LockAsync(input);
        }

        [HttpPost("findByUserName")]
        [SwaggerOperation(summary: "通过用户名查找用户", Tags = new[] { "Users" })]
        public Task<IdentityUserDto> FindByUserNameAsync(FindByUserNameInput input)
        {
            return _userAppService.FindByUserNameAsync(input);
        }

        [HttpPost("myProfile")]
        [SwaggerOperation(summary: "获取个人信息", Tags = new[] { "Users" })]
        public Task<MyProfileOutput> MyProfileAsync()
        {
            return _userAppService.MyProfileAsync();
        }

        [HttpPost("needChangePassword")]
        [SwaggerOperation(summary: "是否需要修改密码", Tags = new[] { "Users" })]
        public Task<NeedChangePasswordOutput> NeedChangePasswordAsync()
        {
            return _userAppService.NeedChangePasswordAsync();
        }
    }
}
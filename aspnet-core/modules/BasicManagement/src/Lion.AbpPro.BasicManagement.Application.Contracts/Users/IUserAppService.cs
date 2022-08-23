using Lion.AbpPro.BasicManagement.Users.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Account;
using Volo.Abp.Identity;

namespace Lion.AbpPro.BasicManagement.Users
{
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询用户
        /// </summary>
        Task<PagedResultDto<IdentityUserDto>> ListAsync(PagingUserListInput input);

        /// <summary>
        /// 用户导出列表
        /// </summary>
        Task<ActionResult> ExportAsync(PagingUserListInput input);
        
        /// <summary>
        /// 新增用户
        /// </summary>
        Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input);

        /// <summary>
        /// 更新用户
        /// </summary>
        Task<IdentityUserDto> UpdateAsync(UpdateUserInput input);

        /// <summary>
        /// 删除用户
        /// </summary>
        Task DeleteAsync(IdInput input);


        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        Task<ListResultDto<IdentityRoleDto>> GetRoleByUserId(IdInput input);

        /// <summary>
        /// 修改密码
        /// </summary>
        Task<bool> ChangePasswordAsync(ChangePasswordInput input);

        /// <summary>
        /// 锁定用户
        /// </summary>
        Task LockAsync(LockUserInput input);
    }
}
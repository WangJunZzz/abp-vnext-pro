using System;
using System.Threading.Tasks;
using CompanyName.ProjectName.Users.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace CompanyName.ProjectName.Users
{
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<IdentityUserDto>> ListAsync(PagingUserListInput input);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdentityUserDto> UpdateAsync(UpdateUserInput input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(Guid id);


        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ListResultDto<IdentityRoleDto>> GetRoleByUserId(Guid userId);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(ChangePasswordInput input);

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task LockAsync(LockUserInput input);
    }
}
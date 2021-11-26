using System;
using System.Threading.Tasks;
using Lion.AbpPro.Users.Dtos;
using Lion.AbpPro.Extension.Customs.Dtos;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Lion.AbpPro.Users
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
        Task DeleteAsync(IdInput input);


        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<IdentityRoleDto>> GetRoleByUserId(IdInput input);

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
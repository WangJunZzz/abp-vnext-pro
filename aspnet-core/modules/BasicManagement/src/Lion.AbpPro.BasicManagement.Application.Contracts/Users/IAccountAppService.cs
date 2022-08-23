using Lion.AbpPro.BasicManagement.Users.Dtos;

namespace Lion.AbpPro.BasicManagement.Users
{
    public interface IAccountAppService: IApplicationService
    {
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        Task<LoginOutput> LoginAsync(LoginInput input);
    }
}

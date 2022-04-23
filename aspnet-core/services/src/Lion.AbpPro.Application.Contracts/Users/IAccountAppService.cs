
using System.Threading.Tasks;
using Lion.AbpPro.Users.Dtos;
using Volo.Abp.Application.Services;



namespace Lion.AbpPro.Users
{
    public interface IAccountAppService: IApplicationService
    {
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LoginOutput> LoginAsync(LoginInput input);
        
        /// <summary>
        /// identityServer4第三方登录
        /// </summary>
        /// <returns></returns>
        Task<LoginOutput> Id4LoginAsync(string accessToken);

        /// <summary>
        /// github第三方登录
        /// </summary>
        /// <param name="code">授权码</param>
        /// <returns></returns>
        Task<LoginOutput> GithubLoginAsync(string code);

    }
}

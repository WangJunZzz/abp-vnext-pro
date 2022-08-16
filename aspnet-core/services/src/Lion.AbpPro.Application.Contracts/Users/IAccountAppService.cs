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
    }
}

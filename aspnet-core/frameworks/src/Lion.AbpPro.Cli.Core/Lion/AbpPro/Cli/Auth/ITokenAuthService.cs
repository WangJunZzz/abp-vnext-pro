namespace Lion.AbpPro.Cli.Auth;

public interface ITokenAuthService
{
    /// <summary>
    /// 设置token
    /// </summary>
    Task SetAsync(string token);
    
    /// <summary>
    /// 获取token
    /// </summary>
    Task<string> GetAsync();
}
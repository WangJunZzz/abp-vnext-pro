using JetBrains.Annotations;
using MyCSharp.HttpUserAgentParser.Providers;
using Volo.Abp.AspNetCore.WebClientInfo;

namespace Lion.AbpPro.AspNetCore;

/// <summary>
/// 真实IP地址提供程序,支持代理服务器场景
/// </summary>
public class RealIpHttpContextWebClientInfoProvider : HttpContextWebClientInfoProvider
{
    private const string XForwardedForHeader = "X-Forwarded-For";


    public RealIpHttpContextWebClientInfoProvider(
        ILogger<HttpContextWebClientInfoProvider> logger,
        IHttpContextAccessor httpContextAccessor,
        IHttpUserAgentParserProvider httpUserAgentParser)
        : base(logger, httpContextAccessor, httpUserAgentParser)
    {
    }

    /// <summary>
    /// 获取客户端IP地址,优先从X-Forwarded-For头部获取
    /// </summary>
    /// <returns>客户端IP地址</returns>
    protected override string GetClientIpAddress()
    {
        try
        {
            var httpContext = HttpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return null;
            }

            var headers = httpContext.Request?.Headers;
            if (headers != null && headers.ContainsKey(XForwardedForHeader))
            {
                // 从X-Forwarded-For获取真实客户端IP
                var forwardedIp = headers[XForwardedForHeader].FirstOrDefault();
                if (!string.IsNullOrEmpty(forwardedIp))
                {
                    httpContext.Connection.RemoteIpAddress = IPAddress.Parse(forwardedIp);
                }
            }

            return httpContext.Connection?.RemoteIpAddress?.ToString();
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "获取客户端IP地址时发生异常");
            return null;
        }
    }
}
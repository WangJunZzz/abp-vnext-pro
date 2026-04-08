using JetBrains.Annotations;
using MyCSharp.HttpUserAgentParser.Providers;
using System.Net;
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
                return null;

            string realIp = null;

            // 1. 优先从 X-Forwarded-For 获取真实IP
            if (httpContext.Request.Headers.TryGetValue(XForwardedForHeader, out var forwardedIps))
            {
                realIp = forwardedIps.FirstOrDefault()?.Split(',', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Trim();
            }

            // 2. 取不到就用默认 RemoteIpAddress
            if (string.IsNullOrEmpty(realIp))
            {
                realIp = httpContext.Connection.RemoteIpAddress?.ToString();
            }

            if (string.IsNullOrEmpty(realIp))
                return null;

            // 3. 关键：处理 ::ffff:192.168.1.1 这种格式
            if (IPAddress.TryParse(realIp, out var ipAddress))
            {
                if (ipAddress.IsIPv4MappedToIPv6)
                {
                    realIp = ipAddress.MapToIPv4().ToString();
                }
            }

            return realIp;
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "获取客户端IP地址时发生异常");
            return null;
        }
    }
}
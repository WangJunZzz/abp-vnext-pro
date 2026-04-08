using IPTools.Core;
using Microsoft.Extensions.Options;
using UAParser;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Json;

namespace Lion.AbpPro.BasicManagement.AuditLogs;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IAuditLogInfoToAuditLogConverter))]
public class AbpProAuditLogInfoToAuditLogConverter : AuditLogInfoToAuditLogConverter
{
    public AbpProAuditLogInfoToAuditLogConverter(
        IGuidGenerator guidGenerator, 
        IExceptionToErrorInfoConverter exceptionToErrorInfoConverter,
        IJsonSerializer jsonSerializer,
        IOptions<AbpExceptionHandlingOptions> exceptionHandlingOptions,
        AuditLogEntityTypeFullNameConverter auditLogEntityTypeFullNameConverter) 
        : base(
            guidGenerator, 
            exceptionToErrorInfoConverter, 
            jsonSerializer, 
            exceptionHandlingOptions, 
            auditLogEntityTypeFullNameConverter)
    {
    }

    public override Task<AuditLog> ConvertAsync(AuditLogInfo auditLogInfo)
    {
        auditLogInfo.ClientIpAddress = GetClientIpAddress(auditLogInfo);
        auditLogInfo.BrowserInfo = GetBrowserInfo(auditLogInfo);
        return base.ConvertAsync(auditLogInfo);
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    /// <param name="auditLogInfo">审计日志信息</param>
    /// <returns>IP地址</returns>
    protected virtual string GetClientIpAddress(AuditLogInfo auditLogInfo)
    {
        var ipAddr = auditLogInfo.ClientIpAddress;
        IpInfo location;
        if (auditLogInfo.ClientIpAddress == "127.0.0.1" || auditLogInfo.ClientIpAddress?.ToLower() == "localhost")
        {
            return "本地-本机";
        }

        try
        {
            location = IpTool.Search(ipAddr);
        }
        catch
        {
            location = new IpInfo() { IpAddress  = ipAddr, Province = string.Empty, City = "未知地区" };
        }


        return string.Format($"{location.IpAddress}({location.Province}{location.City})");
    }

    /// <summary>
    /// 获取浏览器信息
    /// </summary>
    /// <param name="auditLogInfo">审计日志信息</param>
    /// <returns>浏览器信息</returns>
    protected virtual string GetBrowserInfo(AuditLogInfo auditLogInfo)
    {
        try
        {
            if (auditLogInfo.BrowserInfo.IsNotNullOrWhiteSpace())
            {
                var uaParser = Parser.GetDefault();
                return uaParser.Parse(auditLogInfo.BrowserInfo).UA.ToString();
            }
        }
        catch
        {
            return "Other";
        }
        return auditLogInfo.BrowserInfo;
    }
}
using System.Runtime.ExceptionServices;
using Volo.Abp.ExceptionHandling;

namespace System;

/// <summary>
/// 异常操作扩展
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// 格式化异常消息
    /// </summary>
    /// <param name="e">异常对象</param>
    /// <param name="isHideStackTrace">是否隐藏异常规模信息</param>
    /// <returns>格式化后的异常信息字符串</returns>
    public static string FormatMessage(this Exception e, bool isHideStackTrace = false)
    {
        var sb = new StringBuilder();
        var appString = string.Empty;
        if (e == null) return sb.ToString();
        if (e is IHasErrorCode errorCodeException)
        {
            sb.AppendLine($"{appString}异常Code：{errorCodeException.Code}");
        }

        sb.AppendLine($"{appString}异常消息：{e.Message}");
        sb.AppendLine($"{appString}异常类型：{e.GetType().FullName}");
        sb.AppendLine($"{appString}异常方法：{(e.TargetSite == null ? null : e.TargetSite.Name)}");
        sb.AppendLine($"{appString}异常源：{e.Source}");
        if (!isHideStackTrace && e.StackTrace != null)
        {
            sb.AppendLine($"{appString}异常堆栈：{e.StackTrace}");
        }

        if (e.InnerException == null) return sb.ToString();
        sb.AppendLine($"{appString}内部异常：");
        return sb.ToString();
    }


    /// <summary>
    /// 如果条件成立，则抛出异常
    /// </summary>
    public static void ThrowIf(this Exception exception, bool isThrow)
    {
        if (isThrow)
        {
            throw exception;
        }
    }

    /// <summary>
    /// 如果条件成立，则抛出异常
    /// </summary>
    public static void ThrowIf(this Exception exception, Func<bool> isThrowFunc)
    {
        if (isThrowFunc())
        {
            throw exception;
        }
    }
}
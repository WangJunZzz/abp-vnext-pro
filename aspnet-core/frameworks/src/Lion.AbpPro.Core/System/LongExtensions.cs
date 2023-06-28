using System.Collections.Specialized;
using System.Reflection;

namespace System;

/// <summary>
/// long<see cref="long"/>的扩展辅助操作方法
/// </summary>
public static class LongExtensions
{
    /// <summary>
    ///  时间戳转本地时间-时间戳精确到秒
    /// </summary> 
    public static DateTime ToLocalTimeDateBySeconds(this long unix)
    {
        var dto = DateTimeOffset.FromUnixTimeSeconds(unix);
        return dto.ToLocalTime().DateTime;
    }

    /// <summary>
    ///  时间戳转本地时间-时间戳精确到毫秒
    /// </summary> 
    public static DateTime ToLocalTimeDateByMilliseconds(this long unix)
    {
        var dto = DateTimeOffset.FromUnixTimeMilliseconds(unix);
        return dto.ToLocalTime().DateTime;
    }
}
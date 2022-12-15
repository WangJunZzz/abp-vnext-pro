namespace System;

/// <summary>
/// int 扩展方法
/// </summary>
public static class Int32Extensions
{
    /// <summary>
    /// 将 <see cref="DateTimeExtensions.ToYyyyMmDd"/> 反转换.
    /// 例如 20210826 => 2021-08-26
    /// </summary>
    public static DateTime YyyyMmDdToTime(this int value)
    {
        var canParse = DateTime.TryParseExact(value.ToString(), "yyyyMMdd", null, DateTimeStyles.None,
            out var result);
        if (!canParse)
        {
            throw new Exception("not can parse");
        }

        return result;
    }
}
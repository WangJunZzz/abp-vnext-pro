namespace System;

/// <summary>
/// Decimal扩展操作类
/// </summary>
public static class DecimalExtensions
{
    /// <summary>
    /// 移除小数点后面的零
    /// </summary>
    public static decimal TrimEndZero(this decimal number)
    {
        var str = number.ToString(CultureInfo.InvariantCulture);
        if (str.Contains('.'))
        {
            return Convert.ToDecimal(str.TrimEnd('0').TrimEnd('.'));
        }

        return Convert.ToDecimal(str);
    }

    /// <summary>
    /// 移除小数点后面的零
    /// </summary>
    public static decimal? TrimEndZero(this decimal? number)
    {
        if (number.HasValue)
        {
            return TrimEndZero(number.Value);
        }

        return null;
    }
}
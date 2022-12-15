namespace System.Collections.Generic;

/// <summary>
/// 集合扩展方法
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// 如果条件成立，添加项
    /// </summary>
    public static void AddIf<T>(this ICollection<T> collection, T value, bool flag)
    {
        if (collection is null) throw new ArgumentNullException();
        if (flag)
        {
            collection.Add(value);
        }
    }

    /// <summary>
    /// 如果条件成立，添加项
    /// </summary>
    public static void AddIf<T>(this ICollection<T> collection, T value, Func<bool> func)
    {
        if (collection is null) throw new ArgumentNullException();
        if (func())
        {
            collection.Add(value);
        }
    }

    /// <summary>
    /// 如果不存在，添加项
    /// </summary>
    public static void AddIfNotExist<T>(this ICollection<T> collection, T value, Func<T, bool> existFunc = null)
    {
        if (collection is null) throw new ArgumentNullException();
        var exists = existFunc == null ? collection!.Contains(value) : collection!.Any(existFunc);
        if (!exists)
        {
            collection!.Add(value);
        }
    }

    /// <summary>
    /// 如果不为空，添加项
    /// </summary>
    public static void AddIfNotNull<T>(this ICollection<T> collection, T value) where T : class
    {
        if (collection is null) throw new ArgumentNullException();
        if (value != null)
        {
            collection!.Add(value);
        }
    }

    /// <summary>
    /// 获取对象，不存在对使用委托添加对象
    /// </summary>
    public static T GetOrAdd<T>(this ICollection<T> collection, Func<T, bool> selector, Func<T> factory)
    {
        if (collection is null) throw new ArgumentNullException();
        T item = collection.FirstOrDefault(selector);
        if (item == null)
        {
            item = factory();
            collection.Add(item);
        }

        return item;
    }


    /// <summary>
    /// 判断数字集合是否是连续的
    /// </summary>
    /// <returns>如果参数集合为null或空集合，则返回false</returns>
    public static bool IsContinuous(this List<int> numList)
    {
        if (numList == null || numList.Count == 0)
        {
            return false;
        }

        numList.Sort((x, y) => -x.CompareTo(y)); //降序
        bool result = false;
        var totalCount = numList.Count();
        for (int i = 0; i < totalCount - 1; i++)
        {
            result = numList[i] - numList[i + 1] == 1;

            if (!result)
            {
                break;
            }
        }

        return result;
    }
}
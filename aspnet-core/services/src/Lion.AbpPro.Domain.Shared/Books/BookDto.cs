namespace Lion.AbpPro.Books;

/// <summary>
/// 书籍
/// </summary>
public class BookDto 
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }  

    /// <summary>
    /// 编号
    /// </summary>
    public string No { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 价格
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public BookType BookType { get; set; }


    private const string CacheKeyFormat = "i:{0}";

    public static string CalculateCacheKey(Guid id)
    {
        return string.Format(CacheKeyFormat, id);
    }
}
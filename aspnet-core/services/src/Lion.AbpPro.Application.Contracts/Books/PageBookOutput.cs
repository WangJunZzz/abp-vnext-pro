namespace Lion.AbpPro.Books;

/// <summary>
/// 分页查询书籍
/// </summary>
public class PageBookOutput
{
    /// <summary>
    /// 书籍Id
    /// </summary>
    public Guid Id { get; set; }

      
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
    public string BookTypeDescription => BookType.ToDescription();
 
    /// <summary>
    /// 创建时间
    /// </summary>       
    public DateTime CreationTime { get; set; }     
}
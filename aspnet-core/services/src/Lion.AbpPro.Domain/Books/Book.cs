using Lion.AbpPro.Core;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lion.AbpPro.Books;

/// <summary>
/// 书籍
/// </summary>
public class Book : FullAuditedAggregateRoot<Guid>
{
    private Book()
    {
    }

              
    public Book(
        Guid id,
        string no,
        string name,
        decimal price,
        string remark,
        BookType bookType
        ) : base(id)
    {
        SetNo(no);
        SetName(name);
        SetPrice(price);
        SetRemark(remark);
        SetBookType(bookType);
    }
        
    /// <summary>
    /// 编号
    /// </summary>
    public string No { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 价格
    /// </summary>
    public decimal Price { get; private set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; private set; }
    /// <summary>
    /// 类型
    /// </summary>
    public BookType BookType { get; private set; }



    /// <summary>
    /// 设置编号
    /// </summary>        
    private void SetNo(string no)
    {
        Guard.NotNullOrWhiteSpace(no, nameof(no), 128, 0);
        No = no;
    }     

    /// <summary>
    /// 设置名称
    /// </summary>        
    private void SetName(string name)
    {
        Guard.NotNullOrWhiteSpace(name, nameof(name), 128, 0);
        Name = name;
    }     

    /// <summary>
    /// 设置价格
    /// </summary>        
    private void SetPrice(decimal price)
    {
        Price = price;
    }     

    /// <summary>
    /// 设置备注
    /// </summary>        
    private void SetRemark(string remark)
    {
        Guard.Length(remark, nameof(remark), 512, 0);
        Remark = remark;
    }     

    /// <summary>
    /// 设置类型
    /// </summary>
    private void SetBookType(BookType bookType)
    {
        BookType = bookType;
    }
      
    /// <summary>
    /// 更新书籍
    /// </summary> 
    public void Update(
        string no,
        string name,
        decimal price,
        string remark,
        BookType bookType
    )
    {
        SetNo(no);
        SetName(name);
        SetPrice(price);
        SetRemark(remark);
        SetBookType(bookType);
    }
}
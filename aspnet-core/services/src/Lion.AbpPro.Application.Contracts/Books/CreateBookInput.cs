using System.ComponentModel.DataAnnotations;

namespace Lion.AbpPro.Books;

/// <summary>
/// 创建书籍
/// </summary>
public class CreateBookInput
{
    /// <summary>
    /// 编号
    /// </summary>
    [Required(ErrorMessage = "编号不能为空")]
    public string No { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空")]
    public string Name { get; set; }
    /// <summary>
    /// 价格
    /// </summary>
    [Required(ErrorMessage = "价格不能为空")]
    public decimal Price { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }            
    /// <summary>
    /// 类型
    /// </summary>
    public BookType BookType { get; set; }
}
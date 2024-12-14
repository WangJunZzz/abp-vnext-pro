namespace Lion.AbpPro.Books;

/// <summary>
/// 分页查询书籍
/// </summary>
public class PageBookInput : PagingBase
{
      /// <summary>
      /// 开始创建时间
      /// </summary>       
      public DateTime? StartCreationTime { get; set; } 
      
      /// <summary>
      /// 结束创建时间
      /// </summary>       
      public DateTime? EndCreationTime { get; set; } 
}
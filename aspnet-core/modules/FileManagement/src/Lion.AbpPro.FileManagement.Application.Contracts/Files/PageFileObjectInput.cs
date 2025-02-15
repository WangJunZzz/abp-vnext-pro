namespace Lion.AbpPro.FileManagement.Files;

/// <summary>
/// 分页查询文件
/// </summary>
public class PageFileObjectInput : PagingBase
{
    /// <summary>
    /// 开始创建时间
    /// </summary>       
    public DateTime? StartCreationTime { get; set; }

    /// <summary>
    /// 结束创建时间
    /// </summary>       
    public DateTime? EndCreationTime { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }
}
namespace Lion.AbpPro.FileManagement.Files;

/// <summary>
/// 更新文件
/// </summary>
public class UpdateFileInput
{
    /// <summary>
    /// 文件Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 文件Provider
    /// </summary>
    [Required(ErrorMessage = "文件Provider不能为空")]
    public string ProviderKey { get; set; }
    /// <summary>
    /// 文件扩展名
    /// </summary>
    [Required(ErrorMessage = "文件扩展名不能为空")]
    public string FileExtension { get; set; }
    /// <summary>
    /// 二进制数据
    /// </summary>
    [Required(ErrorMessage = "二进制数据不能为空")]
    public long Bytes { get; set; }
    /// <summary>
    /// 文件大小
    /// </summary>
    [Required(ErrorMessage = "文件大小不能为空")]
    public long FileSize { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    [Required(ErrorMessage = "文件名称不能为空")]
    public string ContentType { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    [Required(ErrorMessage = "文件名称不能为空")]
    public string FileName { get; set; }
}
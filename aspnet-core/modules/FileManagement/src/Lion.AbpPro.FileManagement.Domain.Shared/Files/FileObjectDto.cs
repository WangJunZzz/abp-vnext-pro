namespace Lion.AbpPro.FileManagement.Files;

/// <summary>
/// 文件
/// </summary>
public class FileObjectDto 
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
    /// 文件Provider
    /// </summary>
    public string ProviderKey { get; set; }
    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string FileExtension { get; set; }
    
    /// <summary>
    /// 二进制数据
    /// </summary>
    public byte[] Bytes { get; set; }
    /// <summary>
    /// 文件大小
    /// </summary>
    public long FileSize { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    public string ContentType { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }


    private const string CacheKeyFormat = "i:{0}";

    public static string CalculateCacheKey(Guid id)
    {
        return string.Format(CacheKeyFormat, id);
    }
}
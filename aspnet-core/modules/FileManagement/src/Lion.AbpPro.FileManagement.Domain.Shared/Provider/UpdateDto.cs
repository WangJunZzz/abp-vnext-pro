namespace Lion.AbpPro.FileManagement.Provider;

public class UpdateDto
{
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

    public Dictionary<string, object> ExtraProperties { get; set; }
}
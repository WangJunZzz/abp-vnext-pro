namespace Lion.AbpPro.FileManagement.Files.Dto;

public class FileTokenOutput
{
    public string AccessKeyId { get; set; }

    public string AccessKeySecret { get; set; }

    public string Token { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public string Expiration { get; set; }

    public string Bucket { get; set; }
    
    public string Region { get; set; }
}
using Lion.AbpPro.Core;

namespace Lion.AbpPro.FileManagement.Files;

/// <summary>
/// 文件
/// </summary>
public class FileObject : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
   

    public FileObject(
        Guid id,
        string providerKey,
        byte[] bytes,
        long fileSize,
        string contentType,
        string fileName,
        Guid? tenantId = null
    ) : base(id)
    {
        SetProviderKey(providerKey);
        SetBytes(bytes);
        SetFileSize(fileSize);
        SetContentType(contentType);
        SetFileName(fileName);
        TenantId = tenantId;
    }

    public Guid? TenantId { get; private set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; private set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string ContentType { get; private set; }


    /// <summary>
    /// 文件大小
    /// </summary>
    public long FileSize { get; private set; }


    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string FileExtension { get; private set; }

    /// <summary>
    /// 二进制数据
    /// </summary>
    public byte[] Bytes { get; private set; }

    /// <summary>
    /// 文件Provider
    /// </summary>
    public string ProviderKey { get; private set; }


    /// <summary>
    /// 设置文件Provider
    /// </summary>        
    private void SetProviderKey(string providerKey)
    {
        Guard.NotNullOrWhiteSpace(providerKey, nameof(providerKey), 128, 0);
        ProviderKey = providerKey;
    }

    /// <summary>
    /// 设置文件扩展名
    /// </summary>        
    private void SetFileExtension(string fileExtension)
    {
        Guard.NotNullOrWhiteSpace(fileExtension, nameof(fileExtension), 36, 0);
        FileExtension = fileExtension;
    }

    /// <summary>
    /// 设置二进制数据
    /// </summary>        
    private void SetBytes(byte[] bytes)
    {
        Bytes = bytes;
    }

    /// <summary>
    /// 设置文件大小
    /// </summary>        
    private void SetFileSize(long fileSize)
    {
        FileSize = fileSize;
    }

    /// <summary>
    /// 设置文件名称
    /// </summary>        
    private void SetContentType(string contentType)
    {
        Guard.NotNullOrWhiteSpace(contentType, nameof(contentType), 128, 0);
        ContentType = contentType;
    }

    /// <summary>
    /// 设置文件名称
    /// </summary>        
    private void SetFileName(string fileName)
    {
        Guard.NotNullOrWhiteSpace(fileName, nameof(fileName), 128, 0);
        SetFileExtension(Path.GetExtension(fileName));
        FileName = fileName;
    }

    /// <summary>
    /// 更新文件
    /// </summary> 
    public void Update(
        string providerKey,
        string fileExtension,
        byte[] bytes,
        long fileSize,
        string contentType,
        string fileName
    )
    {
        SetProviderKey(providerKey);
        SetFileExtension(fileExtension);
        SetBytes(bytes);
        SetFileSize(fileSize);
        SetContentType(contentType);
        SetFileName(fileName);
    }
}
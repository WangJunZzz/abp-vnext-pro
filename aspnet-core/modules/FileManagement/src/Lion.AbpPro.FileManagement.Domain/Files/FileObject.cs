using JetBrains.Annotations;
using Lion.AbpPro.Core;

namespace Lion.AbpPro.FileManagement.Files;

/// <summary>
/// 文件
/// </summary>
public class FileObject : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public FileObject(
        Guid id,
        string fileName,
        long fileSize,
        string contentType,
        Guid? tenantId = null
    ) : base(id)
    {
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
    /// 文件大小
    /// </summary>
    public long FileSize { get; private set; }

    /// <summary>
    /// 文件类型
    /// </summary>
    public string ContentType { get; private set; }


    /// <summary>
    /// 设置文件大小
    /// </summary>        
    public void SetFileSize(long fileSize)
    {
        FileSize = fileSize;
    }

    /// <summary>
    /// 设置文件类型
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
        FileName = fileName;
    }

    /// <summary>
    /// 更新文件
    /// </summary> 
    public void Update(
        long fileSize,
        string contentType,
        string fileName
    )
    {
        SetFileSize(fileSize);
        SetContentType(contentType);
        SetFileName(fileName);
    }
}
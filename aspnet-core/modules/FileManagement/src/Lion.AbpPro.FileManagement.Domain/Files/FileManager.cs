namespace Lion.AbpPro.FileManagement.Files;

public class FileManager : DomainService
{
    private readonly IBlobContainer<AbpProFileManagementContainer> _blobContainer;
    private readonly FileObjectManager _fileObjectManager;

    public FileManager(IBlobContainer<AbpProFileManagementContainer> blobContainer, FileObjectManager fileObjectManager)
    {
        _blobContainer = blobContainer;
        _fileObjectManager = fileObjectManager;
    }

    /// <summary>
    /// 创建文件
    /// </summary>
    public virtual async Task<FileObjectDto> CreateAsync(
        Guid id,
        string fileName,
        long fileSize,
        string contentType,
        byte[] content,
        bool overwrite = false
    )
    {
        var entity = await _fileObjectManager.CreateAsync(id, fileName, fileSize, contentType, content, overwrite);
        await _blobContainer.SaveAsync(id.ToString(), content, true);
        return entity;
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    public virtual async Task DeleteAsync(Guid id)
    {
        await _fileObjectManager.DeleteAsync(id);
        await _blobContainer.DeleteAsync(id.ToString());
    }
}
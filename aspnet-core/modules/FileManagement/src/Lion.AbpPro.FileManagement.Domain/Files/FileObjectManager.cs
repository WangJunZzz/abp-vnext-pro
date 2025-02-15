using Volo.Abp.ObjectMapping;

namespace Lion.AbpPro.FileManagement.Files;

public class FileObjectManager : DomainService
{
    private readonly IFileObjectRepository _fileObjectRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly ICurrentTenant _currentTenant;

    public FileObjectManager(IFileObjectRepository iIFileObjectRepository, IObjectMapper objectMapper, ICurrentTenant currentTenant)
    {
        _fileObjectRepository = iIFileObjectRepository;
        _objectMapper = objectMapper;
        _currentTenant = currentTenant;
    }

    public async Task<List<FileObjectDto>> GetListAsync(string fileName, DateTime? startDateTime = null, DateTime? endDateTime = null, int maxResultCount = 10, int skipCount = 0)
    {
        return await _fileObjectRepository.GetListAsync(fileName, startDateTime, endDateTime, maxResultCount, skipCount);
    }

    public async Task<long> GetCountAsync(string fileName, DateTime? startDateTime = null, DateTime? endDateTime = null)
    {
        return await _fileObjectRepository.GetCountAsync(fileName, startDateTime, endDateTime);
    }

    /// <summary>
    /// 创建文件
    /// </summary>
    public async Task<FileObjectDto> CreateAsync(
        Guid id,
        string fileName,
        byte[] bytes,
        long fileSize,
        string contentType,
        string providerKey
    )
    {
        var entity = new FileObject(id, providerKey, bytes, fileSize, contentType, fileName, _currentTenant.Id);
        entity = await _fileObjectRepository.InsertAsync(entity);
        return _objectMapper.Map<FileObject, FileObjectDto>(entity);
    }


    /// <summary>
    /// 删除文件
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _fileObjectRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException($"文件不存在");
        await _fileObjectRepository.DeleteAsync(entity);
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    public async Task<FileObjectDto> GetAsync(Guid id)
    {
        var entity = await _fileObjectRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException($"文件不存在");
        return _objectMapper.Map<FileObject, FileObjectDto>(entity);
    }
}
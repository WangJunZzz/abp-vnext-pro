using Volo.Abp.ObjectMapping;

namespace Lion.AbpPro.FileManagement.Files;

public class FileObjectManager : DomainService
{
    private readonly IFileObjectRepository _fileObjectRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly ICurrentTenant _currentTenant;

    public FileObjectManager(IFileObjectRepository fileObjectRepository,
        IObjectMapper objectMapper,
        ICurrentTenant currentTenant)
    {
        _fileObjectRepository = fileObjectRepository;
        _objectMapper = objectMapper;
        _currentTenant = currentTenant;
    }

    public async Task<List<FileObjectDto>> GetListAsync(string fileName, DateTime? startDateTime = null, DateTime? endDateTime = null, int maxResultCount = 10, int skipCount = 0)
    {
        var list = await _fileObjectRepository.GetListAsync(fileName, startDateTime, endDateTime, maxResultCount, skipCount);
        return _objectMapper.Map<List<FileObject>, List<FileObjectDto>>(list);
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
        long fileSize,
        string contentType,
        byte[] content,
        bool overwrite = false
    )
    {
        var entity = await _fileObjectRepository.FindAsync(fileName);
        if (entity != null)
        {
            if (!overwrite)
            {
                throw new DomainFileManagementException(FileManagementErrorCodes.FileAlreadyExist);
            }

            entity.SetFileSize(fileSize);
            await _fileObjectRepository.UpdateAsync(entity);
        }
        else
        {
            entity = new FileObject(id, fileName, fileSize, contentType, CurrentTenant?.Id);
            await _fileObjectRepository.InsertAsync(entity);
        }

        return _objectMapper.Map<FileObject, FileObjectDto>(entity);
    }


    /// <summary>
    /// 删除文件
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _fileObjectRepository.FindAsync(id);
        if (entity == null) throw new DomainFileManagementException(FileManagementErrorCodes.FileNotFound);
        await _fileObjectRepository.DeleteAsync(entity);
    }

    /// <summary>
    /// 获取文件
    /// </summary>
    public async Task<FileObjectDto> GetAsync(Guid id)
    {
        var entity = await _fileObjectRepository.FindAsync(id);
        if (entity == null) throw new DomainFileManagementException(FileManagementErrorCodes.FileNotFound);
        return _objectMapper.Map<FileObject, FileObjectDto>(entity);
    }
}
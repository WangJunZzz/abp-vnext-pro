using Lion.AbpPro.FileManagement.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Lion.AbpPro.FileManagement.Files;

/// <summary>
/// 文件
/// </summary>
[Authorize]
public class FileAppService : ApplicationService, IFileAppService
{
    private readonly FileObjectManager _fileObjectManager;
    private readonly IFileProvider _fileProvider;

    public FileAppService(FileObjectManager fileObjectManager, IFileProvider fileProvider)
    {
        _fileObjectManager = fileObjectManager;
        _fileProvider = fileProvider;
    }

    /// <summary>
    /// 分页查询文件
    /// </summary>      
    public async Task<PagedResultDto<PageFileObjectOutput>> PageAsync(PageFileObjectInput input)
    {
        var result = new PagedResultDto<PageFileObjectOutput>();
        var totalCount = await _fileObjectManager.GetCountAsync(input.FileName, input.StartCreationTime, input.EndCreationTime);
        result.TotalCount = totalCount;
        if (totalCount <= 0) return result;
        var list = await _fileObjectManager.GetListAsync(input.FileName, input.StartCreationTime, input.EndCreationTime, input.PageSize, input.SkipCount);
        result.Items = ObjectMapper.Map<List<FileObjectDto>, List<PageFileObjectOutput>>(list);
        return result;
    }

    public async Task<List<UploadOutput>> UploadAsync(List<IFormFile> files)
    {
        var result = new List<UploadOutput>();
        foreach (var formFile in files)
        {
            try
            {
                var item = new UploadOutput();
                // 获取文件的二进制数据
                using var memoryStream = new MemoryStream();
                await formFile.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                // 这里可以对 fileBytes 进行后续处理，例如保存到数据库或文件系统等
                // 示例：将文件保存到数据库
                var updateResult = await _fileProvider.UploadAsync(new UpdateDto()
                {
                    FileName = formFile.FileName,
                    Bytes = fileBytes,
                    ContentType = formFile.ContentType,
                    FileSize = formFile.Length
                });

                result.Add(new UploadOutput()
                {
                    Id = updateResult.Id,
                    Name = updateResult.FileName,
                    Path = updateResult.FilePath,
                    Success = true
                });
            }
            catch (Exception e)
            {
                Logger.LogError(e, "上传文件失败");
                result.Add(new UploadOutput()
                {
                    Id = Guid.Empty,
                    Name = formFile.FileName,
                    Path = string.Empty,
                    Success = false
                });
            }
        }

        return result;
    }


    /// <summary>
    /// 删除文件
    /// </summary>
    public Task DeleteAsync(DeleteFileObjectInput input)
    {
        return _fileObjectManager.DeleteAsync(input.Id);
    }

    public async Task<GetFileObjectOutput> GetAsync(GetFileObjectInput input)
    {
        var file = await _fileObjectManager.GetAsync(input.Id);
        return ObjectMapper.Map<FileObjectDto, GetFileObjectOutput>(file);
    }
}
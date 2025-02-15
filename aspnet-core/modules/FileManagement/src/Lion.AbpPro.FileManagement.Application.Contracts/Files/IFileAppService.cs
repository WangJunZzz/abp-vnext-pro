using Microsoft.AspNetCore.Http;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.FileManagement.Files;

/// <summary>
/// 文件
/// </summary>
public interface IFileAppService : IApplicationService
{
    /// <summary>
    /// 分页查询文件
    /// </summary>
    Task<PagedResultDto<PageFileObjectOutput>> PageAsync(PageFileObjectInput input);

    /// <summary>
    /// 上传文件
    /// </summary>
    Task<List<UploadOutput>> UploadAsync(List<IFormFile> files);
    
    /// <summary>
    /// 删除文件
    /// </summary>
    Task DeleteAsync(DeleteFileObjectInput input);
    
    Task<GetFileObjectOutput> GetAsync(GetFileObjectInput input);
}
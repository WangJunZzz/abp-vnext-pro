using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

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
    Task UploadAsync(List<IFormFile> files);
    
    /// <summary>
    /// 删除文件
    /// </summary>
    Task DeleteAsync(DeleteFileObjectInput input);
    
    
    /// <summary>
    /// 下载文件
    /// </summary>
    Task<RemoteStreamContent> DownloadAsync(DownloadFileObjectInput input);
}
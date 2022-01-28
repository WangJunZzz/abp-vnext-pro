using System.Collections.Generic;
using System.Threading.Tasks;
using Lion.AbpPro.FileManagement.Files.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.FileManagement.Files;

public interface IFileAppService : IApplicationService
{
    Task<FileTokenOutput> GetFileTokenAsync();
    Task CreateAsync(CreateFileInput input);

    Task<PagedResultDto<PagingFileOutput>> PagingAsync(PagingFileInput input);
}
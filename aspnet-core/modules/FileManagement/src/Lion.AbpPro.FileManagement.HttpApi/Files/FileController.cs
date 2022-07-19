namespace Lion.AbpPro.FileManagement.Files;

[Route("Files")]
public class FileController: FileManagementController, IFileAppService
{
    private readonly IFileAppService _fileAppService;

    public FileController(IFileAppService fileAppService)
    {
        _fileAppService = fileAppService;
    }

    [HttpGet("getFileToken")]
    [SwaggerOperation(summary: "获取上传文件临时Token", Tags = new[] { "Files" })]
    public Task<FileTokenOutput> GetFileTokenAsync()
    {
        return _fileAppService.GetFileTokenAsync();
    }

    [HttpPost("create")]
    [SwaggerOperation(summary: "创建文件", Tags = new[] { "Files" })]
    public Task CreateAsync(CreateFileInput input)
    {
        return _fileAppService.CreateAsync(input);
    }

    [HttpPost("page")]
    [SwaggerOperation(summary: "分页查询", Tags = new[] { "Files" })]
    public Task<PagedResultDto<PagingFileOutput>> PagingAsync(PagingFileInput input)
    {
        return _fileAppService.PagingAsync(input);
    }
}
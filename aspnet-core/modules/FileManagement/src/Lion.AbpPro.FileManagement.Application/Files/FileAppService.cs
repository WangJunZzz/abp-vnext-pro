namespace Lion.AbpPro.FileManagement.Files;

[Authorize]
public class FileAppService : FileManagementAppService, IFileAppService
{
    private readonly FileManager _fileManager;
    private readonly IConfiguration _configuration;

    public FileAppService(FileManager fileManager, IConfiguration configuration)
    {
        _fileManager = fileManager;
        _configuration = configuration;
    }
    
    public async Task<FileTokenOutput> GetFileTokenAsync()
    {
        // 如何设置 sts https://help.aliyun.com/document_detail/100624.html
        var regionId = _configuration.GetValue<string>("AliYun:OSS:RegionId");
        var accessKeyId = _configuration.GetValue<string>("AliYun:OSS:AccessKeyId");
        var accessKeySecret = _configuration.GetValue<string>("AliYun:OSS:AccessKeySecret");
        var profile = DefaultProfile.GetProfile(regionId, accessKeyId, accessKeySecret);
        var client = new DefaultAcsClient(profile);
        var request = new AssumeRoleRequest()
        {
            RoleArn = _configuration.GetValue<string>("AliYun:OSS:RoleArn"),
            RoleSessionName = "YH.Wms"
        };
        var response = client.GetAcsResponse(request);

        var result = new FileTokenOutput()
        {
            AccessKeyId = response.Credentials.AccessKeyId,
            AccessKeySecret = response.Credentials.AccessKeySecret,
            Token = response.Credentials.SecurityToken,
            Expiration = response.Credentials.Expiration,
            Region = _configuration.GetValue<string>("AliYun:OSS:RegionId"),
            Bucket = _configuration.GetValue<string>("AliYun:OSS:ContainerName"),
        };
        return await Task.FromResult(result);
    }

    [Authorize(FileManagementPermissions.FileManagement.Upload)]
    public async Task CreateAsync(CreateFileInput input)
    {
        await _fileManager.CreateAsync(input.FileName, input.FilePath);
    }

    public async Task<PagedResultDto<PagingFileOutput>> PagingAsync(PagingFileInput input)
    {
        var result = new PagedResultDto<PagingFileOutput>();
        var totalCount = await _fileManager.CountAsync(input.Filter);
        result.TotalCount = totalCount;
        if (totalCount <= 0) return result;
        var entities = await _fileManager.PagingAsync(input.Filter, input.PageSize,
            input.SkipCount);
        result.Items = ObjectMapper.Map<List<File>, List<PagingFileOutput>>(entities);
        return result;
    }
}
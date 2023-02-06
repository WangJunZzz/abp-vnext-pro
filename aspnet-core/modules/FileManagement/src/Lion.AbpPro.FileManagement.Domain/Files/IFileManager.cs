namespace Lion.AbpPro.FileManagement.Files;

public interface IFileManager
{
    Task CreateAsync(string fileName, string filePath);

    Task<List<File>> PagingAsync(
        string filter = null,
        int maxResultCount = 10,
        int skipCount = 0);

    Task<long> CountAsync(string filter = null);
    IAbpLazyServiceProvider LazyServiceProvider { get; set; }
    IServiceProvider ServiceProvider { get; set; }
}
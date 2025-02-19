namespace Lion.AbpPro.FileManagement.Files;

public interface IFileObjectRepository : IBasicRepository<FileObject, Guid>
{
    Task<List<FileObject>> GetListAsync(string fileName, DateTime? startDateTime = null, DateTime? endDateTime = null, int maxResultCount = 10, int skipCount = 0);

    Task<long> GetCountAsync(string fileName, DateTime? startDateTime = null, DateTime? endDateTime = null);
    
    Task<FileObject> FindAsync(string fileName);
    
}
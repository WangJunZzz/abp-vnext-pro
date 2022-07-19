namespace Lion.AbpPro.FileManagement.Files;

public class File: FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; private set; }
    
    public string FileName { get; private set; }
    
    public string FilePath { get; private set; }

    private File()
    {
        
    }

    public File(Guid id, Guid? tenantId, string fileName, string filePath) : base(id)
    {
        TenantId = tenantId;
        FileName = fileName;
        FilePath = filePath;
    }
}
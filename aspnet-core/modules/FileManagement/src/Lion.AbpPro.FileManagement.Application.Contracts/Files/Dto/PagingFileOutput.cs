namespace Lion.AbpPro.FileManagement.Files.Dto;

public class PagingFileOutput
{
    public Guid Id { get; set; }
    
    public Guid? TenantId { get; set; }

    public string FileName { get; set; }

    public string FilePath { get; set; }

    public DateTime CreationTime { get; set; }
}
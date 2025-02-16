namespace Lion.AbpPro.FileManagement.Files;

public class UploadOutput
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool Success { get; set; }
    

    public string Path { get; set; }
}
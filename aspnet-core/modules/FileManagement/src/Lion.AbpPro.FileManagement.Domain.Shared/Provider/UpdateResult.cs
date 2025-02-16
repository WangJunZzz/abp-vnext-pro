namespace Lion.AbpPro.FileManagement.Provider;

public class UpdateResult
{
    public Guid Id { get; set; }

    public string FileName { get; set; }

    public string FilePath { get; set; }

    public UpdateResult()
    {
        ExtraProperties = new Dictionary<string, object>();
    }

    public Dictionary<string, object> ExtraProperties { get; set; }
}
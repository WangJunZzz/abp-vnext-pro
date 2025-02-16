namespace Lion.AbpPro.FileManagement.Provider;

public interface IFileProvider
{
    Task<UpdateResult> UploadAsync(UpdateDto input);
}
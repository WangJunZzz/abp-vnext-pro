using Lion.AbpPro.FileManagement.Files;
using Volo.Abp.Guids;

namespace Lion.AbpPro.FileManagement.Provider;

public class DatabaseFileProvider : IFileProvider, ITransientDependency
{
    public const string Name = nameof(DatabaseFileProvider);
    private readonly FileObjectManager _fileObjectManager;
    private readonly IGuidGenerator _guidGenerator;

    public DatabaseFileProvider(FileObjectManager fileObjectManager, IGuidGenerator guidGenerator)
    {
        _fileObjectManager = fileObjectManager;
        _guidGenerator = guidGenerator;
    }

    public async Task<UpdateResult> UploadAsync(UpdateDto input)
    {
        var id = _guidGenerator.Create();
        var fileName = id + "_" + input.FileName;
        await _fileObjectManager.CreateAsync(id, fileName, input.Bytes, input.FileSize, input.ContentType, Name);
        return new UpdateResult()
        {
            Id = id,
            FileName = fileName,
            FilePath = fileName
        };
    }
}
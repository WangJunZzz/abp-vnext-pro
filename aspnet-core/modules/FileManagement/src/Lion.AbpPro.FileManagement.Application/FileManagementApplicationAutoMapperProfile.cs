using Lion.AbpPro.FileManagement.Files;

namespace Lion.AbpPro.FileManagement;

public class FileManagementApplicationAutoMapperProfile : Profile
{
    public FileManagementApplicationAutoMapperProfile()
    {
        CreateMap<Files.FileObjectDto, PageFileObjectOutput>();
    }
}
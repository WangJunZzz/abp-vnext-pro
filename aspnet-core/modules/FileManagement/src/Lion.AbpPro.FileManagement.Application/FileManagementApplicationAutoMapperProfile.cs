namespace Lion.AbpPro.FileManagement;

public class FileManagementApplicationAutoMapperProfile : Profile
{
    public FileManagementApplicationAutoMapperProfile()
    {
        CreateMap<Lion.AbpPro.FileManagement.Files.File, PagingFileOutput>();
    }
}
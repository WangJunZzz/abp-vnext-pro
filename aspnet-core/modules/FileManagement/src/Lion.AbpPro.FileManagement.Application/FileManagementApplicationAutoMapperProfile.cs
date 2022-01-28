using AutoMapper;
using Lion.AbpPro.FileManagement.Files;
using Lion.AbpPro.FileManagement.Files.Dto;

namespace Lion.AbpPro.FileManagement;

public class FileManagementApplicationAutoMapperProfile : Profile
{
    public FileManagementApplicationAutoMapperProfile()
    {
        CreateMap<File, PagingFileOutput>();
    }
}
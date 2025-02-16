using AutoMapper;
using Lion.AbpPro.FileManagement.Files;

namespace Lion.AbpPro.FileManagement
{
    public class FileManagementDomainAutoMapperProfile : Profile
    {
        public FileManagementDomainAutoMapperProfile()
        {
            CreateMap<FileObject, FileObjectDto>();
        }
    }
}
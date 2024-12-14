using AutoMapper;
using Lion.AbpPro.Books;

namespace Lion.AbpPro;

public class AbpProDomainAutoMapperProfile: Profile 
{
    public AbpProDomainAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
    }
}
using Lion.AbpPro.Books;

namespace Lion.AbpPro
{
    public class AbpProApplicationAutoMapperProfile : Profile
    {
        public AbpProApplicationAutoMapperProfile()
        {
            CreateMap<BookDto, PageBookOutput>();
        }
    }
}

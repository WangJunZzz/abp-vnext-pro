using AutoMapper;
using CompanyNameProjectName.Dtos.Users;
using Volo.Abp.Identity;

namespace CompanyNameProjectName
{
    public class CompanyNameProjectNameApplicationAutoMapperProfile : Profile
    {
        public CompanyNameProjectNameApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<IdentityUser, LoginOutputDto>();
        }
    }
}

using AutoMapper;
using CompanyName.ProjectName.Users.Dtos;

namespace CompanyName.ProjectName.Users.Mappers
{
    public class UserApplicationAutoMapperProfile:Profile
    {
        public UserApplicationAutoMapperProfile()
        {
            CreateMap<Volo.Abp.Identity.IdentityUser, LoginOutput>();
        }
    }
}
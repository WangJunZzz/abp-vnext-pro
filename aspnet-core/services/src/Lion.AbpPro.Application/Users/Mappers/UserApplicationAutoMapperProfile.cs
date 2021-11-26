using AutoMapper;
using Lion.AbpPro.Users.Dtos;

namespace Lion.AbpPro.Users.Mappers
{
    public class UserApplicationAutoMapperProfile:Profile
    {
        public UserApplicationAutoMapperProfile()
        {
            CreateMap<Volo.Abp.Identity.IdentityUser, LoginOutput>();
        }
    }
}
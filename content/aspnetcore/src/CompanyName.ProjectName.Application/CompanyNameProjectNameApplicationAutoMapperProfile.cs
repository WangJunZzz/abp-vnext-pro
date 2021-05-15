using AutoMapper;
using CompanyNameProjectName.Audits.Dtos;
using CompanyNameProjectName.Dtos.Users;
using Volo.Abp.AuditLogging;
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
            #region 用户
            CreateMap<IdentityUser, LoginOutputDto>();
            #endregion


            #region 审计日志
            CreateMap<AuditLog, QueryAuditLogOutput>();
            CreateMap<EntityChange, QueryEntityChangeOutput>();
            CreateMap<EntityPropertyChange, PropertyChangesDto>();
            #endregion
        }
    }
}

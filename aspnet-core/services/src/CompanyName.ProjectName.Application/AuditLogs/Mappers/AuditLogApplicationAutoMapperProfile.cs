using AutoMapper;
using Volo.Abp.AuditLogging;

namespace CompanyName.ProjectName.AuditLogs.Mappers
{
    public class AuditLogApplicationAutoMapperProfile:Profile
    {
        public AuditLogApplicationAutoMapperProfile()
        {
            CreateMap<AuditLog, GetAuditLogPageListOutput>();
        }   
    }
}
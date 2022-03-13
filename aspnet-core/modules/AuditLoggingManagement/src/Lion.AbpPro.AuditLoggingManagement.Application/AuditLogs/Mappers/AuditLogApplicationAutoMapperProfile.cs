using AutoMapper;
using Lion.AbpPro.AuditLoggingManagement.AuditLogs.Dtos;
using Volo.Abp.AuditLogging;

namespace Lion.AbpPro.AuditLoggingManagement.AuditLogs.Mappers
{
    public class AuditLogApplicationAutoMapperProfile : Profile
    {
        public AuditLogApplicationAutoMapperProfile()
        {
            CreateMap<AuditLog, AuditLogOutput>();
            CreateMap<AuditLogAction, AuditLogActionOutput>();
            CreateMap<EntityChange, EntityChangeOutput>();
            CreateMap<EntityPropertyChange, EntityPropertyChangeOutput>();
        }
    }
}

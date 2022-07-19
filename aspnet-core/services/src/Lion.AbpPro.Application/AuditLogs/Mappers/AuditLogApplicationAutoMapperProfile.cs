namespace Lion.AbpPro.AuditLogs.Mappers
{
    public class AuditLogApplicationAutoMapperProfile:Profile
    {
        public AuditLogApplicationAutoMapperProfile()
        {
            CreateMap<AuditLog, GetAuditLogPageListOutput>();
        }   
    }
}
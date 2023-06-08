using Volo.Abp.Auditing;

namespace Lion.AbpPro.BasicManagement.AuditLogs;

public class PagingEntityChangeOutput
{
    public Guid Id { get;  set; }
    
    public Guid AuditLogId { get; set; }

    public Guid? TenantId { get; set; }

    public string ChangeTime { get; set; }
    
    public EntityChangeType ChangeType { get; set; }
    
    public string ChangeTypeDescription { get; set; }

    public Guid? EntityTenantId { get; set; }

    public string EntityId { get; set; }

    public string EntityTypeFullName { get; set; }

    public List<PagingEntityPropertyChangeOutput> PropertyChanges { get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; set; }
}
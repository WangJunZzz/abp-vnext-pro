namespace Lion.AbpPro.BasicManagement.AuditLogs;

public class PagingAuditLogActionOutput
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public Guid AuditLogId { get; set; }

    public string ServiceName { get; set; }

    public string MethodName { get; set; }

    public string Parameters { get; set; }

    public string ExecutionTime { get; set; }

    public int ExecutionDuration { get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; set; }
}
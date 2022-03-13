namespace Lion.AbpPro.AuditLoggingManagement.AuditLogs.Dtos
{
    public class AuditLogActionOutput
    {
        public Guid? Id { set; get; }

        public Guid? TenantId { set; get; }

        public string ServiceName { set; get; }

        public string MethodName { set; get; }

        public string Parameters { set; get; }

        public DateTime ExecutionTime { set; get; }

        public int ExecutionDuration { set; get; }    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

namespace Lion.AbpPro.AuditLoggingManagement.AuditLogs.Dtos
{
    public class EntityChangeOutput
    {
        public Guid AuditLogId { get; set; }

        public Guid? TenantId { get; set; }

        public DateTime ChangeTime { get; set; }

        public EntityChangeType ChangeType { get; set; }

        public Guid? EntityTenantId { get; set; }

        public string EntityId { get; set; }

        public string EntityTypeFullName { get; set; }

        public ICollection<EntityPropertyChangeOutput> PropertyChanges { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}

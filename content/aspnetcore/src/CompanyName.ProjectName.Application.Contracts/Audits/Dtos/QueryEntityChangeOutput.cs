using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

namespace CompanyNameProjectName.Audits.Dtos
{
    public class QueryEntityChangeOutput : EntityDto<Guid>
    {
        public Guid AuditLogId
        {
            get;
            set;
        }

        public Guid? TenantId
        {
            get;
            set;
        }

        public DateTime ChangeTime
        {
            get;
            set;
        }

        public EntityChangeType ChangeType
        {
            get;
            set;
        }

        public Guid? EntityTenantId
        {
            get;
            set;
        }

        public string EntityId
        {
            get;
            set;
        }

        public string EntityTypeFullName
        {
            get;
            set;
        }

        public ICollection<PropertyChangesDto> PropertyChanges
        {
            get;
            set;
        }

        public ExtraPropertyDictionary ExtraProperties
        {
            get;
            set;
        }

    }

    public class PropertyChangesDto : EntityDto<Guid>
    {
        public Guid? TenantId
        {
            get;
            set;
        }

        public Guid EntityChangeId
        {
            get;
            set;
        }

        public string NewValue
        {
            get;
            set;
        }

        public string OriginalValue
        {
            get;
            set;
        }

        public string PropertyName
        {
            get;
            set;
        }

        public string PropertyTypeFullName
        {
            get;
            set;
        }

    }
}

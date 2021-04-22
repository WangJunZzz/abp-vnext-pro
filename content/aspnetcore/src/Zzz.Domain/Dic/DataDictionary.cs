using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Data;
using System.ComponentModel.DataAnnotations;

namespace Zzz.Dic
{
    public class DataDictionary : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(64)]
        public virtual  string Name { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(256)]
        public virtual  string Description { get; set; }

        [Required]
        public virtual  bool IsDeleted { get;  set; }

        public virtual  Guid? TenantId { get; private set; }



        public virtual List<DataDictionaryDetail> DataDictionaryDetails { get; protected set; }


        protected DataDictionary()
        {

        }

        public DataDictionary(Guid id, string name,  Guid? tenantId,  string description)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            TenantId = tenantId;
            Id = id;
            Name = name;
            Description = description;
            IsDeleted = false;
            DataDictionaryDetails = new List<DataDictionaryDetail>();
        }

        public void SetProperties(string name,string description)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Name = name;
            Description = description;
        }

        public void AddDataDictionaryDetail(DataDictionaryDetail detail)
        {
            var dictionaryDetail = DataDictionaryDetails.FirstOrDefault(e => e.Label == detail.Label);
            if (dictionaryDetail == null)
            {
                DataDictionaryDetails.Add(detail);
            }
            else
            {
                dictionaryDetail.SetProperties(detail.Label, detail.Value, detail.Sort);
            }
        }

        public void DeleteAll()
        {
            IsDeleted = true;
            foreach (var item in DataDictionaryDetails)
            {
                item.Delete();
            }
        }

        public void DeleteItem(Guid itemId)
        {
            var item= DataDictionaryDetails.Find(e => e.Id == itemId);
            item.Delete();
        }
    }
}

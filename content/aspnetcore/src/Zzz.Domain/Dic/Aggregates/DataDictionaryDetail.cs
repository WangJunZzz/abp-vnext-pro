using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Zzz.Dic.MaxLengths;

namespace Zzz.Dic
{
    public class DataDictionaryDetail : Entity<Guid>, ISoftDelete
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(DataDictionaryMaxLength.Label)]
        public virtual string Label { get; protected set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(DataDictionaryMaxLength.Value)]
        public virtual string Value { get; protected set; }

        [Required]
        public virtual bool IsDeleted { get;  set; }

        public virtual int Sort { get; protected set; }

        public virtual Guid DataDictionaryId { get; protected set; }

        protected DataDictionaryDetail()
        {


        }
        public DataDictionaryDetail(Guid id, string label, string value, int sort)
        {
          
            Id = id;
            Label = label;
            Value = value;
            Sort = sort;
        }

        public void SetProperties(string lable,string value,int sort)
        {
            Label = lable;
            Value = value;
            Sort = sort;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class UpdateDataDictinaryInput
    {
        [Required] 
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string DisplayText { get; set; }
        public string Description { get; set; }
    }
}

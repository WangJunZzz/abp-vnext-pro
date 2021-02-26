using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zzz.DTOs.Dic
{
    public class CreateDataDictionaryDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage ="Name Not Be Null")]
        public  string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public  string Description { get; set; }


        //public  List<CreateDataDictionaryDetailDto> DataDictionaryDetails { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (DataDictionaryDetails == null || DataDictionaryDetails.Count == 0)
        //    {
        //        yield return new ValidationResult("DataDictionaryDetails Not Be Null");
        //    }
        //}
    }
}

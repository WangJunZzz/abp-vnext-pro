using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zzz.DTOs.Dic
{
    public class CreateDataDictionaryDetailDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Label Not Be Null")]
        public  string Label { get;  set; }

        [Required(ErrorMessage = "Value Not Be Null")]
        public  string Value { get;  set; }

        public  int Sort { get; set; }
    }
}

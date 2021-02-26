using System;
using System.ComponentModel.DataAnnotations;

namespace Zzz.DTOs.Dic
{
    public class UpdataDataDictionaryDto
    {

        

        public Guid Id { get; set; }



        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "Name Not Be Null")]
        public  string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public  string Description { get; set; }

    }
}

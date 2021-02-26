using System;
using System.Collections.Generic;
using System.Text;

namespace Zzz.DTOs.Dic
{
    public class GetDataDictionaryDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}

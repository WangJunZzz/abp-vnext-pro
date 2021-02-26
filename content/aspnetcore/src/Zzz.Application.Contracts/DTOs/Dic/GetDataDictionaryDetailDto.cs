using System;
using System.Collections.Generic;
using System.Text;

namespace Zzz.DTOs.Dic
{
    public class GetDataDictionaryDetailDto
    {
        
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public int Sort { get; set; }
    }
}

using System;
using Lion.AbpPro.Extension.Customs.Dtos;


namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class PagingDataDictionaryDetailInput : PagingBase
    {
        public Guid DataDictionaryId { get; set; }
        public string Filter { get; set; }
    }
}
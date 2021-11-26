using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class PagingDataDictionaryInput : PagingBase
    {
        public string Filter { get; set; }
    }
}
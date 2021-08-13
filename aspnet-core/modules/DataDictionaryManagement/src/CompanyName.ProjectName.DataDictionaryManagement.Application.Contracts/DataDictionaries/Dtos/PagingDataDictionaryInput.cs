using CompanyName.ProjectName.Extensions.Customs;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class PagingDataDictionaryInput : PagingBase
    {
        public string Filter { get; set; }
    }
}
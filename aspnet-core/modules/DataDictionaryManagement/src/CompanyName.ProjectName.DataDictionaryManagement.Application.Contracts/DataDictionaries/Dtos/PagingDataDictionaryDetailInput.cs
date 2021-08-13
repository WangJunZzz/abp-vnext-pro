using System;
using CompanyName.ProjectName.Extensions.Customs;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class PagingDataDictionaryDetailInput : PagingBase
    {
        public Guid DataDictionaryId { get; set; }
        public string Filter { get; set; }
    }
}
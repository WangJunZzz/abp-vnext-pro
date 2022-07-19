namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class CreateDataDictinaryInput
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string DisplayText { get; set; }
        public string Description { get; set; }
    }
}
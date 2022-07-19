namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class UpdateDataDictinaryInput
    {
        [Required] 
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string DisplayText { get; set; }
        public string Description { get; set; }
    }
}

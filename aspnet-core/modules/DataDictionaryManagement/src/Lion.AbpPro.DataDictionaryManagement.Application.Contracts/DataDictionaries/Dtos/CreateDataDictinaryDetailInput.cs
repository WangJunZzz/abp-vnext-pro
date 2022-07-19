namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class CreateDataDictinaryDetailInput
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string DisplayText { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
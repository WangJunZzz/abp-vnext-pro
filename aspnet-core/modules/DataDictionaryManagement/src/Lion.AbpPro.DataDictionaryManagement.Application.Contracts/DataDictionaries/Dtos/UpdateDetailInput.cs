namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class UpdateDetailInput
    {
        [Required] public Guid DataDictionaryId { get; set; }

        [Required] public Guid Id { get; set; }

        [Required] public string DisplayText { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }
    }
}


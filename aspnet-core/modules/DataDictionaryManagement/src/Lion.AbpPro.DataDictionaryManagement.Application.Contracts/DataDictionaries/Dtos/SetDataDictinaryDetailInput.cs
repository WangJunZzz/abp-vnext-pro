namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class SetDataDictinaryDetailInput
    {
        public Guid DataDictionaryId { get; set; }


        public Guid DataDictionayDetailId { get; set; }

        [Required] public bool IsEnabled { get; set; }
    }
}
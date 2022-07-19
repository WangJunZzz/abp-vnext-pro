namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class PagingDataDictionaryOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 字典编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
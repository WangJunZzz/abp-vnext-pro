namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class PagingDataDictionaryDetailOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 所属字典Id
        /// </summary>
        public Guid DataDictionaryId { get; set; }

        /// <summary>
        /// 字典明细编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 展现列表时排序用
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 英文显示名
        /// </summary>
        public string DisplayText { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 启/停用(默认启用)
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
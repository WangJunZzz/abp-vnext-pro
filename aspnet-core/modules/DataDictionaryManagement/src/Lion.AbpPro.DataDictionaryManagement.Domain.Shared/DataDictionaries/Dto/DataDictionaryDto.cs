namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dto
{
    public class DataDictionaryDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId { get; set; }

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
        
        /// <summary>
        /// 字典明细集合
        /// </summary>
        public List<DataDictionaryDetailDto> Details { get;  set; }

        public DataDictionaryDto()
        {
            Details = new List<DataDictionaryDetailDto>();
        }
        
        private const string CacheKeyFormat = "i:{0},c:{1}";

        public static string CalculateCacheKey(Guid? id, string code)
        {
            return string.Format(CacheKeyFormat,
                id?.ToString() ?? "null",
                (code.IsNullOrWhiteSpace() ? "null" : code));
        } 
    }
}
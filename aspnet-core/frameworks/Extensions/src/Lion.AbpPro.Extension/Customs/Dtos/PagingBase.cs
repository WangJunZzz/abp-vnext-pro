namespace Lion.AbpPro.Extension.Customs.Dtos
{

    
    /// <summary>
    /// 分页查询时使用的Dto类型
    /// </summary>
    public class PagingBase : IValidatableObject
    {
        public const int MaxPageSize = 100000;
        
        /// <summary>
        /// 当前页面.默认从1开始
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页多少条.每页显示多少记录
        /// </summary>
        public int PageSize { get; set; } = 10;
        
        /// <summary>
        /// 跳过多少条
        /// </summary>
        public int SkipCount => (PageIndex - 1) * PageSize;

        protected PagingBase()
        {
        }
        
        public PagingBase(int pageIndex = 1, int pageSize = 10)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
        
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PageIndex < 1)
            {
                yield return new ValidationResult(
                    "起始页必须大于等于1",
                    new[] { "PageIndex"}
                );
            }
            
            if (PageSize > MaxPageSize)
            {
                yield return new ValidationResult(
                    $"每页最大记录数不能超过'{MaxPageSize}'",
                    new[] { "PageSize"}
                );
            }
        }
    }
}
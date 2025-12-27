using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.Ddd.Application.Contracts
{
    /// <summary>
    /// 分页查询请求DTO，包含时间范围和自定义排序功能
    /// </summary>
    public class AbpProPagedInput : PagedAndSortedResultRequestDto, IAbpProPagedInput
    {
        /// <summary>
        /// 当前页面.默认从1开始
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页多少条.每页显示多少记录
        /// </summary>
        public int PageSize { get; set; } = 10;

        public override int SkipCount => (PageIndex - 1) * PageSize;

        public override int MaxResultCount => PageSize;

        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        

        /// <summary>
        /// 排序方向（ascending/descending）
        /// </summary>
        public string IsAsc { get; set; }

        /// <summary>
        /// 过滤
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 是否为升序排序
        /// </summary>
        public bool IsAscending => string.Equals(IsAsc, "ASC", StringComparison.OrdinalIgnoreCase);

        public override string Sorting { get; set; }

    }
}
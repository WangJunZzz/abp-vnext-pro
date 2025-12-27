using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.Ddd.Application.Contracts
{
    /// <summary>
    /// 带时间范围的分页查询请求接口
    /// </summary>
    public interface IAbpProPageTimeInput: IPagedAndSortedResultRequest
    {
        /// <summary>
        /// 查询开始时间
        /// </summary>
        DateTime? StartTime { get; set; }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        DateTime? EndTime { get; set; }
    }
}

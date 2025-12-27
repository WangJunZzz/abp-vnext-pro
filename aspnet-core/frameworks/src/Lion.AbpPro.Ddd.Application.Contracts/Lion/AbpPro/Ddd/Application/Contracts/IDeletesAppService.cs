using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.Ddd.Application.Contracts
{
    /// <summary>
    /// 批量删除服务接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IDeletesAppService<in TKey> : IDeleteAppService<TKey>, IApplicationService, IRemoteService
    {
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="ids">要删除的实体ID集合</param>
        /// <returns>删除操作的异步任务</returns>
        Task DeleteAsync(IEnumerable<TKey> ids);
    }
}

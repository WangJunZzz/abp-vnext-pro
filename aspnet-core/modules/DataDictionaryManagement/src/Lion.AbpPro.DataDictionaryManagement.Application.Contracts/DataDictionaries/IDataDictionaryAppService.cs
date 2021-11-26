using System.Threading;
using System.Threading.Tasks;
using Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries
{
    public interface IDataDictionaryAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询字典项
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedResultDto<PagingDataDictionaryOutput>> GetPagingListAsync(
            PagingDataDictionaryInput input,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询字典项明细
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedResultDto<PagingDataDictionaryDetailOutput>> GetPagingDetailListAsync(
            PagingDataDictionaryDetailInput input,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <returns></returns>
        Task CreateAsync(CreateDataDictinaryInput input);

        /// <summary>
        /// 新增字典明细
        /// </summary>
        Task CreateDetailAsync(CreateDataDictinaryDetailInput input);

        /// <summary>
        /// 设置字典明细状态
        /// </summary>
        Task SetStatus(SetDataDictinaryDetailInput input);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Aggregates;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Dtos;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryAppService : DataDictionaryManagementAppService, IDataDictionaryAppService
    {
        /// <summary>
        ///  注意 为了快速直接注入仓库层 规范上是不允许的
        ///  这里注入仓储也只是为了查询分页
        ///  如果是其他的操作全部通过对应manger进行操作
        /// </summary>
        private readonly IDataDictionaryRepository _dataDictionaryRepository;
        private readonly DataDictionaryManager _dataDictionaryManager;
        
        public DataDictionaryAppService(
            IDataDictionaryRepository dataDictionaryRepository,
            DataDictionaryManager dataDictionaryManager)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
            _dataDictionaryManager = dataDictionaryManager;
        }

        /// <summary>
        /// 分页查询字典项
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PagingDataDictionaryOutput>> GetPagingListAsync(
            PagingDataDictionaryInput input,
            CancellationToken cancellationToken = default)
        {
            var result = new PagedResultDto<PagingDataDictionaryOutput>();
            var totalCount = await _dataDictionaryRepository.GetPagingCountAsync(input.Filter, cancellationToken);
            result.TotalCount = totalCount;
            if (totalCount <= 0) return result;

            var entities = await _dataDictionaryRepository.GetPagingListAsync(input.Filter, input.PageSize,
                input.SkipCount, false, cancellationToken);
            result.Items = ObjectMapper.Map<List<DataDictionary>, List<PagingDataDictionaryOutput>>(entities);

            return result;
        }

        /// <summary>
        /// 分页查询字典项明细
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PagingDataDictionaryDetailOutput>> GetPagingDetailListAsync(
            PagingDataDictionaryDetailInput input,
            CancellationToken cancellationToken = default)
        {
            var entity = await _dataDictionaryRepository.FindByIdAsync(input.DataDictionaryId, true, cancellationToken);
            var details = entity.Details.Take(input.PageSize).Skip(input.SkipCount).ToList();
            return new PagedResultDto<PagingDataDictionaryDetailOutput>(
                entity.Details.Count,
                ObjectMapper.Map<List<DataDictionaryDetail>, List<PagingDataDictionaryDetailOutput>>(details));
        }
        
        
        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <returns></returns>
        public Task CreateAsync(CreateDataDictinaryInput input)
        {
            return _dataDictionaryManager.CreateAsync(input.Code, input.DisplayText, input.Description);
        }

        /// <summary>
        /// 新增字典明细
        /// </summary>
        public  Task CreateDetailAsync(CreateDataDictinaryDetailInput input)
        {
            return _dataDictionaryManager.CreateDetailAsync(input.Id, input.Code, input.DisplayText, input.Description,
                input.Order);
        }

        /// <summary>
        /// 设置字典明细状态
        /// </summary>
        public  Task SetStatus(SetDataDictinaryDetailInput input)
        {
            return _dataDictionaryManager.SetStatus(input.DataDictionaryId, input.DataDictionayDetailId,
                input.IsEnabled);
        }
    }
}
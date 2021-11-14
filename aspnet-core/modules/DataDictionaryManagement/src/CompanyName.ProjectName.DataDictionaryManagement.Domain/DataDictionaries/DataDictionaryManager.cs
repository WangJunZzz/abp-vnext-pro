using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Aggregates;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Dto;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Exceptions;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Services;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries
{
   public class DataDictionaryManager : DataDictionaryDomainService
    {
        private readonly IDataDictionaryRepository _dataDictionaryRepository;
        private readonly IDistributedCache<DataDictionaryDto> _cache;

        public DataDictionaryManager(
            IDataDictionaryRepository dataDictionaryRepository,
            IDistributedCache<DataDictionaryDto> cache)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
            _cache = cache;
        }

        public async Task<DataDictionaryDto> FindByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var cacheKey = DataDictionaryDto.CalculateCacheKey(id, null);
            return await _cache.GetOrAddAsync(cacheKey,
                async () =>
                {
                    var entity =
                        await _dataDictionaryRepository.FindByIdAsync(id, true,
                            cancellationToken);
                    return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(entity);
                }, token: cancellationToken);
        }

        public async Task<DataDictionaryDto> FindByCodeAsync(
            string code,
            CancellationToken cancellationToken = default)
        {
            var cacheKey = DataDictionaryDto.CalculateCacheKey(null, code);
            return await _cache.GetOrAddAsync(cacheKey,
                async () =>
                {
                    var entity =
                        await _dataDictionaryRepository.FindByCodeAsync(code, true,
                            cancellationToken);
                    return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(entity);
                }, token: cancellationToken);
        }


        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <param name="code"></param>
        /// <param name="displayText"></param>
        /// <param name="description"></param>
        public Task<DataDictionary> CreateAsync(string code, string displayText, string description)
        {
            var entity = new DataDictionary(GuidGenerator.Create(), code, displayText, description);
            return _dataDictionaryRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 新增字典明细
        /// </summary>
        /// <param name="dataDictionaryId"></param>
        /// <param name="code"></param>
        /// <param name="displayText"></param>
        /// <param name="description"></param>
        /// <param name="order"></param>
        /// <exception cref="DataDictionaryDomainException"></exception>
        public async Task<DataDictionary> CreateDetailAsync(Guid dataDictionaryId, string code,
            string displayText,
            string description,
            int order)
        {
            var entity = await _dataDictionaryRepository.FindByIdAsync(dataDictionaryId);
            if (entity == null)
                throw new DataDictionaryDomainException(message: "数据字典不存在");
            if (entity.Details.Any(e => e.Code == code.Trim()))
            {
                throw new DataDictionaryDomainException(message: $"字典项{code}已存在");
            }

            entity.AddDetail(GuidGenerator.Create(), code, displayText, order, description);
            return await _dataDictionaryRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 设置字典明细状态
        /// </summary>
        public async Task<DataDictionary> SetStatus(Guid dataDictionaryId,
            Guid dataDictionayDetailId, bool isEnabled)
        {
            var entity = await _dataDictionaryRepository.FindByIdAsync(dataDictionaryId);
            if (entity == null)
                throw new DataDictionaryDomainException(message: "数据字典不存在");
            var detail = entity.Details.FirstOrDefault(e => e.Id == dataDictionayDetailId);
            if (null == detail)
            {
                throw new DataDictionaryDomainException(message: $"字典项不存在");
            }

            detail.SetIsEnabled(isEnabled);
            return await _dataDictionaryRepository.UpdateAsync(entity);
        }
    }
}
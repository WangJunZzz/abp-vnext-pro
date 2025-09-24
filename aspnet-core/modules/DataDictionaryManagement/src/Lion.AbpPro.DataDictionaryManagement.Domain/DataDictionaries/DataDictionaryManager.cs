namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryManager : DataDictionaryDomainService, IDataDictionaryManager
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


        public virtual async Task<DataDictionaryDto> FindByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var cacheKey = DataDictionaryDto.CalculateCacheKey(id, null);
            return await _cache.GetOrAddAsync
            (
                cacheKey,
                async () =>
                {
                    var entity =
                        await _dataDictionaryRepository.FindByIdAsync
                        (
                            id,
                            true,
                            cancellationToken
                        );
                    return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(entity);
                },
                token: cancellationToken
            );
        }

        public virtual async Task<DataDictionaryDto> FindByCodeAsync(
            string code,
            CancellationToken cancellationToken = default)
        {
            var cacheKey = DataDictionaryDto.CalculateCacheKey(null, code);
            return await _cache.GetOrAddAsync
            (
                cacheKey,
                async () =>
                {
                    var entity =
                        await _dataDictionaryRepository.FindByCodeAsync
                        (
                            code,
                            true,
                            cancellationToken
                        );
                    return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(entity);
                },
                token: cancellationToken
            );
        }


        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <param name="code"></param>
        /// <param name="displayText"></param>
        /// <param name="description"></param>
        public virtual async Task<DataDictionary> CreateAsync(string code, string displayText, string description)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            var entity = await _dataDictionaryRepository.FindByCodeAsync(code);
            if (entity != null) throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryExist);

            entity = new DataDictionary
            (
                GuidGenerator.Create(),
                code,
                displayText,
                description,
                CurrentTenant.Id
            );

            return await _dataDictionaryRepository.InsertAsync(entity);
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
        public virtual async Task<DataDictionary> CreateDetailAsync(
            Guid dataDictionaryId,
            string code,
            string displayText,
            string description,
            int order)
        {
            var entity = await _dataDictionaryRepository.FindByIdAsync(dataDictionaryId);
            if (entity == null)
                throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryNotExist);
            if (entity.Details.Any(e => e.Code == code.Trim()))
            {
                throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryDetailExist);
            }

            entity.AddDetail
            (
                GuidGenerator.Create(),
                code,
                displayText,
                order,
                description
            );
            return await _dataDictionaryRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 设置字典明细状态
        /// </summary>
        public virtual async Task<DataDictionary> SetStatus(
            Guid dataDictionaryId,
            Guid dataDictionaryDetailId,
            bool isEnabled)
        {
            var entity = await _dataDictionaryRepository.FindByIdAsync(dataDictionaryId);
            if (entity == null)
                throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryNotExist);
            var detail = entity.Details.FirstOrDefault(e => e.Id == dataDictionaryDetailId);
            if (null == detail)
            {
                throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryDetailExist);
            }

            detail.SetIsEnabled(isEnabled);
            return await _dataDictionaryRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 更新数据字典明细
        /// </summary>
        public virtual async Task<DataDictionary> UpdateDetailAsync(
            Guid dataDictionaryId,
            Guid dataDictionaryDetailId,
            string displayText,
            string description,
            int order)
        {
            var entity = await _dataDictionaryRepository.FindByIdAsync(dataDictionaryId);
            if (entity == null)
                throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryNotExist);
            var detail = entity.Details.FirstOrDefault(e => e.Id == dataDictionaryDetailId);
            if (null == detail)
            {
                throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryDetailNotExist);
            }

            detail.UpdateDetail
            (
                dataDictionaryDetailId,
                displayText,
                description,
                order
            );
            return await _dataDictionaryRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Guid dataDictionaryId, Guid dataDictionaryDetailId)
        {
            var entity = await _dataDictionaryRepository.FindByIdAsync(dataDictionaryId);
            if (entity == null)
                throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryNotExist);
            var detail = entity.Details.FirstOrDefault(e => e.Id == dataDictionaryDetailId);
            if (null == detail)
            {
                throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryDetailNotExist);
            }
            entity.Details.Remove(detail);
            await _dataDictionaryRepository.UpdateAsync(entity);
        }

        public virtual async Task<DataDictionary> UpdateAsync(
            Guid dataDictionaryId,
            string displayText,
            string description)
        {
            var entity = await _dataDictionaryRepository.FindByIdAsync(dataDictionaryId);
            if (entity == null)
                throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryNotExist);
            entity.Update(displayText, description);
            return await _dataDictionaryRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DeleteDataDictionaryTypeAsync(Guid id)
        {
            var entity = new DataDictionary(GuidGenerator.Create(),GuidGenerator.Create().ToString(),GuidGenerator.Create().ToString(),GuidGenerator.Create().ToString(),CurrentTenant.Id);
            await _dataDictionaryRepository.InsertManyAsync(new List<DataDictionary>(){entity});
            // var entity = await _dataDictionaryRepository.FindByIdAsync(id);
            // if (entity == null)
            //     throw new DataDictionaryDomainException(DataDictionaryManagementErrorCodes.DataDictionaryNotExist);
            // var detail = entity.Details.FirstOrDefault(e => e.DataDictionaryId == id);
            // if (detail != null)
            // {
            //     entity.Details.Remove(detail);
            //     await _dataDictionaryRepository.UpdateAsync(entity);
            // }
            //
            // await _dataDictionaryRepository.DeleteAsync(id);
        }
    }
}
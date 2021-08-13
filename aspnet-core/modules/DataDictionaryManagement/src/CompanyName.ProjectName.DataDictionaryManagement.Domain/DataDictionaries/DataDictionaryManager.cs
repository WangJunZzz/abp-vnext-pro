using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Aggregates;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Exceptions;
using Volo.Abp.Domain.Services;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryManager : DomainService
    {
        private readonly IDataDictionaryRepository _dataDictionaryRepository;

        public DataDictionaryManager(IDataDictionaryRepository dataDictionaryRepository)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
        }

        public Task<DataDictionary> FindByIdAsync(
            Guid id,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return _dataDictionaryRepository.FindByIdAsync(id, includeDetails, cancellationToken);
        }

        public Task<DataDictionary> FindByCodeAsync(
            string code,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return _dataDictionaryRepository.FindByCodeAsync(code, includeDetails, cancellationToken);
        }

        public Task<DataDictionary> FindByDisplayTextAsync(
            string displayText,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return _dataDictionaryRepository.FindByDisplayTextAsync(displayText, includeDetails, cancellationToken);
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
        public async Task<DataDictionary> CreateDetailAsync(Guid dataDictionaryId, string code, string displayText,
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
        public async Task<DataDictionary> SetStatus(Guid dataDictionaryId, Guid dataDictionayDetailId, bool isEnabled)
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
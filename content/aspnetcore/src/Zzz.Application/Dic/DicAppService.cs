using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using Zzz.DTOs.Dic;
using Zzz.DTOs.Public;
using Zzz.Localization;
using Zzz.Permissions;

namespace Zzz.Dic
{
    [Authorize(ZzzPermissions.Dic.Default)]
    public class DicAppService : ApplicationService, IDicAppService
    {
        private readonly IRepository<DataDictionary, Guid> _dataDictionaryRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IStringLocalizer<ZzzResource> _localizer;
      
        public DicAppService(IRepository<DataDictionary, Guid> dataDictionaryRepository, ICurrentUser currentUser, IGuidGenerator guidGenerator, IStringLocalizer<ZzzResource> localizer)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
            _currentUser = currentUser;
            _guidGenerator = guidGenerator;
            _localizer = localizer;
         
        }

        [SwaggerOperation(summary: "获取字典列表", Tags = new[] { "字典" })]
        [Authorize(ZzzPermissions.Dic.Query)]
        public async Task<ApiResult> GetListAsync(string name, int skipCount = 0, int maxResultCount = 10)
        {
            var query = _dataDictionaryRepository.WhereIf(!name.IsNullOrWhiteSpace(), e => e.Name.Contains(name.Trim()));
            var count = await query.CountAsync();
            var items = await query.OrderBy(e => e.Name).PageBy(skipCount, maxResultCount).ToListAsync();
            var dtos = ObjectMapper.Map<List<DataDictionary>, List<GetDataDictionaryDto>>(items);
            var result = new PagedResultDto<GetDataDictionaryDto>(count, dtos);
            return ApiResult.Ok(result);
        }

        [SwaggerOperation(summary: "获取字典详情列表", Tags = new[] { "字典" })]
        [Authorize(ZzzPermissions.Dic.Query)]
        public async Task<ApiResult> GetListDetailAsync(Guid id)
        {
            var entity = await _dataDictionaryRepository.Include(e => e.DataDictionaryDetails).FirstOrDefaultAsync(e => e.Id == id);
            var dtos = ObjectMapper.Map<List<DataDictionaryDetail>, List<GetDataDictionaryDetailDto>>(entity.DataDictionaryDetails);
            var result = new PagedResultDto<GetDataDictionaryDetailDto>(entity.DataDictionaryDetails.Count, dtos);
            return ApiResult.Ok(result);
        }

        [Authorize(ZzzPermissions.Dic.Create)]
        [SwaggerOperation(summary: "新增数据字典", Tags = new[] { "字典" })]
        public async Task<ApiResult> CreateAsync(CreateDataDictionaryDto input)
        {
            var count = await _dataDictionaryRepository.Where(e => e.Name == input.Name.Trim()).CountAsync();
            if (count > 0)
            {
                return ApiResult.Error($"{input.Name} {_localizer["DataExistence"]}");
            }
            var entity = new DataDictionary(_guidGenerator.Create(), input.Name, _currentUser.TenantId, input.Description);
            await _dataDictionaryRepository.InsertAsync(entity);
            return ApiResult.Ok();
        }

        [Authorize(ZzzPermissions.Dic.Create)]
        [SwaggerOperation(summary: "新增数据字典详情", Tags = new[] { "字典" })]
        public async Task<ApiResult> CreateDetailAsync(CreateDataDictionaryDetailDto input)
        {
            var entity = await _dataDictionaryRepository.Include(e => e.DataDictionaryDetails).Where(e => e.Id == input.Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return ApiResult.Error($"{_localizer["DataExistence"]}");
            }

            entity.AddDataDictionaryDetail(new DataDictionaryDetail(_guidGenerator.Create(), input.Label, input.Value, input.Sort));

            await _dataDictionaryRepository.UpdateAsync(entity);
            return ApiResult.Ok();
        }


        [Authorize(ZzzPermissions.Dic.Update)]
        [SwaggerOperation(summary: "更新数据字典", Tags = new[] { "字典" })]
        public async Task<ApiResult> UpdateAsync(UpdataDataDictionaryDto input)
        {
            var entity = await _dataDictionaryRepository.Include(e => e.DataDictionaryDetails).FirstOrDefaultAsync(e => e.Id == input.Id);
            entity.SetProperties(input.Name, input.Description);
            await _dataDictionaryRepository.UpdateAsync(entity);
            return ApiResult.Ok();
        }

        [Authorize(ZzzPermissions.Dic.Update)]
        [SwaggerOperation(summary: "更新数据字典详情", Tags = new[] { "字典" })]
        [UnitOfWork]
        public async Task<ApiResult> UpdateDetailAsync(UpdataDataDictionaryDetailDto input)
        {
            var entity = await _dataDictionaryRepository.Include(e => e.DataDictionaryDetails).FirstOrDefaultAsync(e => e.Id == input.Id);
            var entityDetail = entity.DataDictionaryDetails.FirstOrDefault(e => e.Id == input.DetailId);
            if (entityDetail == null) return ApiResult.Error();
            entityDetail.SetProperties(input.Label, input.Value, input.Sort);
            await _dataDictionaryRepository.UpdateAsync(entity);
            return ApiResult.Ok();
        }

        [Authorize(ZzzPermissions.Dic.Delete)]
        [SwaggerOperation(summary: "删除字典", Tags = new[] { "字典" })]
        public async Task<ApiResult> DeleteAsync(Guid id, Guid? itemId)
        {
            var entity = await _dataDictionaryRepository.Include(e => e.DataDictionaryDetails).FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null) return ApiResult.Error();

            if (itemId.HasValue)
            {
                entity.DeleteItem(itemId.Value);
            }
            else
            {
                entity.DeleteAll();
            }

            await _dataDictionaryRepository.UpdateAsync(entity);
            return ApiResult.Ok();
        }

  
    }
}

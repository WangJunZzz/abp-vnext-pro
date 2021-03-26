using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Zzz.DTOs.Dic;
using Zzz.DTOs.Public;

namespace Zzz.Dic
{
    public interface IDicAppService: IApplicationService
    {
        Task<ApiResult> CreateAsync(CreateDataDictionaryDto input);

        Task<ApiResult> UpdateAsync(UpdataDataDictionaryDto input);

        Task<ApiResult> GetListAsync(string name, int skipCount = 0, int maxResultCount = 10);

        Task<ApiResult> GetListDetailAsync(Guid id);

        Task<ApiResult> CreateDetailAsync(CreateDataDictionaryDetailDto input);

        Task<ApiResult> UpdateDetailAsync(UpdataDataDictionaryDetailDto input);

        Task<ApiResult> DeleteAsync(Guid id, Guid? itemId);
    }
}

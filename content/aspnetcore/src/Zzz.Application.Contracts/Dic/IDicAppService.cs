using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zzz.DTOs.Dic;
using Zzz.DTOs.Public;

namespace Zzz.Dic
{
    public interface IDicAppService
    {
        Task<ApiResult> CreateAsync(CreateDataDictionaryDto input);

        Task<ApiResult> UpdateAsync(UpdataDataDictionaryDto input);
    }
}

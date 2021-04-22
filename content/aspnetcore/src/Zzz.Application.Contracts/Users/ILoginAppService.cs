using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zzz.DTOs.Public;
using Zzz.DTOs.Users;

namespace Zzz.Users
{
    public interface ILoginAppService
    {
        Task<ApiResult> PostAsync(LoginInputDto input);
    }
}

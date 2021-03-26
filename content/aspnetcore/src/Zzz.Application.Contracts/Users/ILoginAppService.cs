using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Zzz.DTOs.Public;
using Zzz.DTOs.Users;

namespace Zzz.Users
{
    public interface ILoginAppService: IApplicationService
    {
        Task<ApiResult> PostAsync(LoginInputDto input);
    }
}

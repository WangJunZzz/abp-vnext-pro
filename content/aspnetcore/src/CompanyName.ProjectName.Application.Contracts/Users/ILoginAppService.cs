using CompanyNameProjectName.Dtos.Users;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;


namespace CompanyNameProjectName.Users
{
    public interface ILoginAppService: IApplicationService
    {
        Task<LoginOutputDto> PostAsync(LoginInputDto input);
    }
}

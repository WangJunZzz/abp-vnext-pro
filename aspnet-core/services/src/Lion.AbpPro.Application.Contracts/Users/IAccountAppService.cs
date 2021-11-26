
using System.Threading.Tasks;
using Lion.AbpPro.Users.Dtos;
using Volo.Abp.Application.Services;



namespace Lion.AbpPro.Users
{
    public interface IAccountAppService: IApplicationService
    {
        Task<LoginOutput> LoginAsync(LoginInput input);

        Task<LoginOutput> StsLoginAsync(string accessToken);
  
    }
}

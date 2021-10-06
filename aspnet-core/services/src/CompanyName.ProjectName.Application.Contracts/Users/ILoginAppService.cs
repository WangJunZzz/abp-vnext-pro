
using System.Threading.Tasks;
using CompanyName.ProjectName.Users.Dtos;
using Volo.Abp.Application.Services;



namespace CompanyName.ProjectName.Users
{
    public interface ILoginAppService: IApplicationService
    {
        Task<LoginOutput> LoginAsync(LoginInput input);

        Task<LoginOutput> StsLoginAsync(string accessToken);
        Task LogoutAsync();
    }
}

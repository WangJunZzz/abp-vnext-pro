using System.Threading.Tasks;
using CompanyName.ProjectName.Users;
using CompanyName.ProjectName.Users.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CompanyName.ProjectName.Controllers.Systems
{
    public class AccountController : ProjectNameController
    {
        private readonly ILoginAppService _loginAppService;

        public AccountController(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }


        [SwaggerOperation(summary: "登录", Tags = new[] {"Account"})]
        public Task<LoginOutput> LoginAsync(LoginInput input)
        {
            return _loginAppService.LoginAsync(input);
        }

        [SwaggerOperation(summary: "登录", Tags = new[] {"Account"})]
        [HttpPost("/api/app/account/login/Sts")]
        public Task<LoginOutput> StsLoginAsync(string accessToken)
        {
            return _loginAppService.StsLoginAsync(accessToken);
        }
        [SwaggerOperation(summary: "登出", Tags = new[] {"Account"})]
        [HttpPost("/api/app/account/logout")]
        public async Task LogoutAsync()
        {
            await _loginAppService.LogoutAsync();
        }
    }
}
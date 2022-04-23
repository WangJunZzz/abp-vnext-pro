using System.Threading.Tasks;
using Lion.AbpPro.Users;
using Lion.AbpPro.Users.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lion.AbpPro.Controllers.Systems
{
    public class AccountController : AbpProController,IAccountAppService
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }


        [SwaggerOperation(summary: "登录", Tags = new[] {"Account"})]
        public Task<LoginOutput> LoginAsync(LoginInput input)
        {
            return _accountAppService.LoginAsync(input);
        }

        [SwaggerOperation(summary: "登录", Tags = new[] {"Account"})]
        [HttpPost("/api/app/account/login/id4")]
        public Task<LoginOutput> Id4LoginAsync(string accessToken)
        {
            return _accountAppService.Id4LoginAsync(accessToken);
        }
        
        [SwaggerOperation(summary: "登录", Tags = new[] {"Account"})]
        [HttpPost("/api/app/account/login/github")]
        public Task<LoginOutput> GithubLoginAsync(string code)
        {
            return _accountAppService.GithubLoginAsync(code);
        }
    }
}
using System.Threading.Tasks;
using CompanyName.ProjectName.Users;
using CompanyName.ProjectName.Users.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CompanyName.ProjectName.Controllers.Systems
{
    public class AccountController : ProjectNameController,IAccountAppService
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
        [HttpPost("/api/app/account/login/Sts")]
        public Task<LoginOutput> StsLoginAsync(string accessToken)
        {
            return _accountAppService.StsLoginAsync(accessToken);
        }
    }
}
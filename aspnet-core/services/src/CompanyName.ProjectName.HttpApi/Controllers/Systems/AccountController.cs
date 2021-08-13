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

       
        [SwaggerOperation(summary: "登录", Tags = new[] { "Account" })]
        public Task<LoginOutput> LoginAsync(LoginInput input)
        {
            return _loginAppService.LoginAsync(input);
        }
    }
}
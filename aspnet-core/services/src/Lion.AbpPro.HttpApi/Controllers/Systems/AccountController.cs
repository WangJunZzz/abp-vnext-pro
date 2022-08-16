using IAccountAppService = Lion.AbpPro.Users.IAccountAppService;

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
        
    }
}
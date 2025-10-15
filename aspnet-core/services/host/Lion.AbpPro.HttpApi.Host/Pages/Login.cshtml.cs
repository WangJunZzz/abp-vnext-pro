
using Lion.AbpPro.BasicManagement.ConfigurationOptions;
using Lion.AbpPro.BasicManagement.Users;
using Lion.AbpPro.BasicManagement.Users.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Timing;


namespace Lion.AbpPro.Pages
{
    public class Login : PageModel
    {
        private readonly IAccountAppService _accountAppService;
        private readonly ILogger<Login> _logger;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly JwtOptions _jwtOptions;
        private readonly IClock _clock;
        public Login(IAccountAppService accountAppService,
            ILogger<Login> logger,
            IHostEnvironment hostEnvironment,
            IOptionsSnapshot<JwtOptions> jwtOptions, 
            IClock clock)
        {
            _accountAppService = accountAppService;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _clock = clock;
            _jwtOptions = jwtOptions.Value;
        }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            string userName = Request.Form["userName"];
            string password = Request.Form["password"];
            if (userName.IsNullOrWhiteSpace() || password.IsNullOrWhiteSpace())
            {
                Response.Redirect("/Login");
                return;
            }

            try
            {
                var options = new CookieOptions
                {
                    Expires = _clock.Now.AddHours(_jwtOptions.ExpirationTime),
                    SameSite = SameSiteMode.Unspecified,
                };


                // 设置cookies domain
                //options.Domain = "AbpPro.cn";


                var result = await _accountAppService.LoginAsync(new LoginInput()
                { Name = userName, Password = password });
                Response.Cookies.Append(AbpProAspNetCoreConsts.DefaultCookieName,
                    result.Token, options);
            }
            catch (Exception e)
            {
                _logger.LogError($"登录失败：{e.Message}");
                Response.Redirect("/Login");
                return;
            }

            Response.Redirect("/monitor");
        }
    }
}
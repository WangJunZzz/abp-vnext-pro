
using Lion.AbpPro.BasicManagement.ConfigurationOptions;
using Lion.AbpPro.BasicManagement.Tenants;
using Lion.AbpPro.BasicManagement.Tenants.Dtos;
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
        private readonly AbpAspNetCoreMultiTenancyOptions _abpAspNetCoreMultiTenancyOptions;
        private readonly IVoloTenantAppService _voloTenantAppService;
        private readonly AbpProMultiTenancyOptions _abpProMultiTenancyOptions;
        public Login(IAccountAppService accountAppService,
            ILogger<Login> logger,
            IHostEnvironment hostEnvironment,
            IOptionsSnapshot<JwtOptions> jwtOptions, 
            IClock clock,
            IOptions<AbpAspNetCoreMultiTenancyOptions> abpAspNetCoreMultiTenancyOptions, 
            IVoloTenantAppService voloTenantAppService, 
            IOptions<AbpProMultiTenancyOptions> abpProMultiTenancyOptions)
        {
            _accountAppService = accountAppService;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _clock = clock;
            _voloTenantAppService = voloTenantAppService;
            _abpProMultiTenancyOptions = abpProMultiTenancyOptions.Value;
            _abpAspNetCoreMultiTenancyOptions = abpAspNetCoreMultiTenancyOptions.Value;
            _jwtOptions = jwtOptions.Value;
            
        }

        public void OnGet()
        {
            ViewData["ErrorMessage"] = null;
            TempData["EnableTenant"] = _abpProMultiTenancyOptions.Enabled;
            Response.Cookies.Delete(_abpAspNetCoreMultiTenancyOptions.TenantKey);
        }

        public async Task OnPost()
        {
            Response.Cookies.Delete(_abpAspNetCoreMultiTenancyOptions.TenantKey);
            TempData["EnableTenant"] = _abpProMultiTenancyOptions.Enabled;
            string tenantName = Request.Form["tenantName"];
            string userName = Request.Form["userName"];
            string password = Request.Form["password"];
            if (userName.IsNullOrWhiteSpace() || password.IsNullOrWhiteSpace())
            {
                // 添加错误提示信息
                TempData["ErrorMessage"] = "用户名和密码不能为空";
                return;
            }

            try
            {
                Guid? tenantId = null;
                // 判断租户是否存在
                if (tenantName.IsNotNullOrWhiteSpace())
                {
                    var tenant = await _voloTenantAppService.FindTenantByNameAsync(new FindTenantByNameInput() { Name = tenantName });
                    if (!tenant.Success)
                    {
                        TempData["ErrorMessage"] = $"租户[{tenantName}]不存在";
                        return;
                    }

                    tenantId = tenant.TenantId;
                }
                
                var options = new CookieOptions
                {
                    Expires = _clock.Now.AddHours(_jwtOptions.ExpirationTime),
                    SameSite = SameSiteMode.Unspecified,
                };

                var result = await _accountAppService.LoginAsync(new LoginInput() { Name = userName, Password = password });
                
                // 清除现有的认证 cookies
                Response.Cookies.Delete(AbpProAspNetCoreConsts.DefaultCookieName);
                Response.Cookies.Append(AbpProAspNetCoreConsts.DefaultCookieName, result.Token, options);
                if (tenantId.HasValue)
                {
                    Response.Cookies.Append(_abpAspNetCoreMultiTenancyOptions.TenantKey, tenantId.ToString(), options);
                }
                
            }
            catch (BusinessException e)
            {
                _logger.LogError($"登录失败：{e.Message}");
                TempData["ErrorMessage"] = $"用户名或者密码错误";
                return;
            }
            catch (Exception e)
            {
                _logger.LogError($"登录失败：{e.Message}");
                TempData["ErrorMessage"] = $"登录失败：{e.Message}";
                return;
            }
            Response.Redirect("/monitor");
        }
    }
}
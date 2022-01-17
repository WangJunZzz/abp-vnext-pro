using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Lion.AbpPro.ConfigurationOptions;
using Lion.AbpPro.Users.Dtos;
using IdentityModel;
using Lion.AbpPro.Extension.Customs.Dtos;
using Lion.AbpPro.Extension.Customs.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;

namespace Lion.AbpPro.Users
{
    public class AccountAppService : AbpProAppService, IAccountAppService
    {
        private readonly IdentityUserManager _userManager;
        private readonly JwtOptions _jwtOptions;

        private readonly Microsoft.AspNetCore.Identity.SignInManager<Volo.Abp.Identity.IdentityUser>
            _signInManager;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICurrentTenant _currentTenant;
        private readonly IHttpContextAccessor _contextAccessor;


        public AccountAppService(
            IdentityUserManager userManager,
            IOptionsSnapshot<JwtOptions> jwtOptions,
            Microsoft.AspNetCore.Identity.SignInManager<IdentityUser> signInManager,
            IHttpClientFactory httpClientFactory, ICurrentTenant currentTenant,
            IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _signInManager = signInManager;
            _httpClientFactory = httpClientFactory;
            _currentTenant = currentTenant;
            _contextAccessor = contextAccessor;
        }


        public async Task<LoginOutput> LoginAsync(LoginInput input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Name, input.Password, false, true);
            if (result.IsNotAllowed)
            {
                throw new UserFriendlyException("当前用户已锁定");
            }

            if (!result.Succeeded)
            {
                throw new UserFriendlyException("用户名或者密码错误");
            }
            
            var user = await _userManager.FindByNameAsync(input.Name);
            return await BuildResult(user);
        }


        public async Task<LoginOutput> StsLoginAsync(string accessToken)
        {
            // 通过access token 获取用户信息
            Dictionary<string, string> headers = new Dictionary<string, string>
                { { "Authorization", $"Bearer {accessToken}" } };
            var response =
                await _httpClientFactory.GetAsync<LoginStsOutput>(HttpClientNameConsts.Sts,
                    "connect/userinfo", headers);
            var user = await _userManager.FindByNameAsync(response.name);
            if (!user.IsActive)
            {
                throw new UserFriendlyException("当前用户已锁定");
            }
            return await BuildResult(user);
        }


        private async Task<LoginOutput> BuildResult(IdentityUser user)
        {
            if (user.LockoutEnabled) throw new UserFriendlyException("当前用户已被锁定");
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0) throw new UserFriendlyException("当前用户未分配角色");
            var token = GenerateJwt(user.Id, user.UserName, user.Name, user.Email,
                user.TenantId.ToString(), roles.ToList());
            var loginOutput = ObjectMapper.Map<IdentityUser, LoginOutput>(user);
            loginOutput.Token = token;
            loginOutput.Roles = roles.ToList();
            return loginOutput;
        }

        /// <summary>
        /// 生成jwt token
        /// </summary>
        /// <returns></returns>
        private string GenerateJwt(Guid userId, string userName, string name, string email,
            string tenantId, List<string> roles)
        {
            var dateNow = DateTime.Now;
            var expirationTime = dateNow + TimeSpan.FromHours(_jwtOptions.ExpirationTime);
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecurityKey);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Audience, _jwtOptions.Audience),
                new Claim(JwtClaimTypes.Issuer, _jwtOptions.Issuer),
                new Claim(AbpClaimTypes.UserId, userId.ToString()),
                new Claim(AbpClaimTypes.Name, name),
                new Claim(AbpClaimTypes.UserName, userName),
                new Claim(AbpClaimTypes.Email, email),
                new Claim(AbpClaimTypes.TenantId, tenantId)
            };

            foreach (var item in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, item));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Zzz.DTOs.Public;
using Zzz.DTOs.Users;
using Zzz.Options;

namespace Zzz.Users
{

    public class LoginAppService : ApplicationService, ILoginAppService
    {
        private readonly IdentityUserManager _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly Microsoft.AspNetCore.Identity.SignInManager<Volo.Abp.Identity.IdentityUser> _signInManager;
        public LoginAppService(
            IdentityUserManager userManager,
            IOptionsSnapshot<JwtOptions> jwtOptions,
            Microsoft.AspNetCore.Identity.SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _signInManager = signInManager;
        }


        [SwaggerOperation(summary: "使用Name或者邮箱登录", Tags = new[] { "账户" })]
        public async Task<ApiResult> PostAsync(LoginInputDto input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Name, input.Password, false, true);
      
            if (!result.Succeeded) return ApiResult.Error();

            var user = await _userManager.FindByNameAsync(input.Name);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(input.Name);
                if (user == null) return ApiResult.Error();
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0) return ApiResult.Error();


            var token = GenerateJwt(user, roles.ToList());
            var loginOutputDto = ObjectMapper.Map<IdentityUser, LoginOutputDto>(user);
            loginOutputDto.Token = token;
            loginOutputDto.Expiration = DateTime.Now.AddHours(_jwtOptions.ExpirationTime);
            return ApiResult.Ok(data: loginOutputDto);
        }



        /// <summary>
        /// 生成jwt token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        private string GenerateJwt(IdentityUser user, List<string> roles)
        {
            var dateNow = DateTime.Now;
            var expirationTime = dateNow + TimeSpan.FromHours(_jwtOptions.ExpirationTime);
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecurityKey);

            var claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Audience, _jwtOptions.Audience),
                    new Claim(JwtClaimTypes.Issuer, _jwtOptions.Issuer),
                    new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                    new Claim(JwtClaimTypes.Name, user.Name),
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim("TenantId", user.TenantId.ToString())
            };

            foreach (var item in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, item));
            }
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);

        }
    }
}

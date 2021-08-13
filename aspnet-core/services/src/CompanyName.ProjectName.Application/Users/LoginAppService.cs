using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CompanyName.ProjectName.ConfigurationOptions;
using CompanyName.ProjectName.QueryManagement.Systems.Users;
using CompanyName.ProjectName.Users.Dtos;
using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;

namespace CompanyName.ProjectName.Users
{
    public class LoginAppService: ProjectNameAppService, ILoginAppService
    {
        private readonly IdentityUserManager _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly Microsoft.AspNetCore.Identity.SignInManager<Volo.Abp.Identity.IdentityUser> _signInManager;
        private readonly IUserFreeSqlRepository _userFreeSqlRepository;
        public LoginAppService(
            IdentityUserManager userManager,
            IOptionsSnapshot<JwtOptions> jwtOptions,
            Microsoft.AspNetCore.Identity.SignInManager<IdentityUser> signInManager, IUserFreeSqlRepository userFreeSqlRepository)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _signInManager = signInManager;
            _userFreeSqlRepository = userFreeSqlRepository;
        }


        public async Task<LoginOutput> LoginAsync(LoginInput input)
        {
            try
            {
                //var s= await _userFreeSqlRepository.GetUserNameByIdAsync(Guid.Parse("39fdb236-a90e-e4b5-02a0-2866a8cf9823"));
                var result = await _signInManager.PasswordSignInAsync(input.Name, input.Password, false, true);
                if (result.IsLockedOut) throw new Exception("当前用户已被锁定");
                if (!result.Succeeded) throw new Exception("用户名或者密码错误");
                var user = await _userManager.FindByNameAsync(input.Name);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles == null || roles.Count == 0) throw new Exception("当前用户未分配角色");
                var token = GenerateJwt(user, roles.ToList());
                var loginOutput = ObjectMapper.Map<IdentityUser, LoginOutput>(user);
                loginOutput.Token = token;
                loginOutput.Roles = roles.ToList();
                return loginOutput;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

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
                new Claim(AbpClaimTypes.UserId, user.Id.ToString()),
                new Claim(AbpClaimTypes.Name, user.Name),
                new Claim(AbpClaimTypes.UserName, user.UserName),
                new Claim(AbpClaimTypes.Email, user.Email),
                new Claim(AbpClaimTypes.TenantId, user.TenantId.ToString())
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
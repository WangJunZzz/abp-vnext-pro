using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzz.DTOs.Users;
using Xunit;
using Xunit.Abstractions;
using Shouldly;
namespace Zzz.Users
{
    public class LoginAppServiceTests : ZzzApplicationTestBase
    {
        private readonly ILoginAppService _loginAppService;

        public LoginAppServiceTests()
        {
            _loginAppService = GetRequiredService<ILoginAppService>();
      
        }

        [Fact]
        public async Task Shuold_Login_Success()
        {
            var loginInfo = new LoginInputDto() { Name = "admin@abp.io", Password = "1q2w3E*" };
            var result = await _loginAppService.PostAsync(loginInfo);
            result.Code.ShouldBe((int)DTOs.Public.ApiCodeEnum.成功);
        }

    }
}

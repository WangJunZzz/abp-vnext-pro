using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Zzz.Dic;
using Zzz.Publics;

namespace Zzz.PublicApi.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : AbpController
    {
        private readonly IConfiguration _configuration;
        private readonly IPublicApiAppService _publicApiAppService;
        public SampleController(
            IConfiguration configuration, 
            IPublicApiAppService publicApiAppService)
        {
            _configuration = configuration;
            _publicApiAppService = publicApiAppService;
        }

        [HttpGet("config")]
        public async Task<IActionResult> GetConfig()
        {
            var test = await _publicApiAppService.TestAsync("测试");
            var result =  _configuration.GetSection("Logging:LogLevel:Default").Value;
            return Ok(result + test);
        }

        [HttpGet("write")]
        [Authorize(Policy = ZzzPublicApiConsts.Policy_Write)]
        public async Task<IActionResult> WriteAsync()
        {
            await Task.CompletedTask;
            return Ok("Write权限通过");
        }

        [HttpGet("read")]
        [Authorize(Policy = ZzzPublicApiConsts.Policy_Read)]
        public async Task<IActionResult> ReadAsync()
        {
            await Task.CompletedTask;
            return Ok("Read权限通过");
        }
    }
}

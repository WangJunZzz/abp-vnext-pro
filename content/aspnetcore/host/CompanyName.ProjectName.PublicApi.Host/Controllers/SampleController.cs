using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace CompanyNameProjectName.PublicApi.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : AbpController
    {
        private readonly IConfiguration _configuration;

        public SampleController(
            IConfiguration configuration)
        {
            _configuration = configuration;
  
        }

        [HttpGet("write")]
        [Authorize(Policy = CompanyNameProjectNamePublicApiConsts.Policy_Write)]
        public async Task<IActionResult> WriteAsync()
        {
            await Task.CompletedTask;
            return Ok("Write权限通过");
        }

        [HttpGet("read")]
        [Authorize(Policy = CompanyNameProjectNamePublicApiConsts.Policy_Read)]
        public async Task<IActionResult> ReadAsync()
        {
            await Task.CompletedTask;
            return Ok("Read权限通过");
        }
    }
}

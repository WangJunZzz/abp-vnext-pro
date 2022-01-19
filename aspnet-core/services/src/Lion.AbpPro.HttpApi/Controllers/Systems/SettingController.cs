using System.Collections.Generic;
using System.Threading.Tasks;
using Lion.AbpPro.Settings;
using Lion.AbpPro.Settings.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lion.AbpPro.Controllers.Systems
{
    [Route("Settings")]
    public class SettingController : AbpProController,ISettingAppService
    {
        private readonly ISettingAppService _settingAppService;

        public SettingController(ISettingAppService settingAppService)
        {
            _settingAppService = settingAppService;
        }

        [HttpPost("all")]
        [SwaggerOperation(summary: "获取所有Setting", Tags = new[] { "Settings" })]
        public async Task<List<SettingOutput>> GetAsync()
        {
            return await _settingAppService.GetAsync();
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "更新Setting", Tags = new[] { "Settings" })]
        public async Task UpdateAsync(UpdateSettingInput input)
        {
            await _settingAppService.UpdateAsync(input);
        }
    }
}
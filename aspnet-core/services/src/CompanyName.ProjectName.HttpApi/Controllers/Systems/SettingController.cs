using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.Settings;
using EasyAbp.Abp.SettingUi;
using EasyAbp.Abp.SettingUi.Dto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CompanyName.ProjectName.Controllers.Systems
{
    [Route("Settings")]
    public class SettingController : ProjectNameController
    {
        private readonly ISettingUiAppService _settingUiAppService;

        public SettingController(ISettingUiAppService settingUiAppService)
        {
            _settingUiAppService = settingUiAppService;
        }

        [HttpPost("all")]
        [SwaggerOperation(summary: "获取所有Setting", Tags = new[] {"Settings"})]
        public async Task<List<SettingGroup>> GetAsync()
        {
            return await _settingUiAppService.GroupSettingDefinitions();
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "更新Setting", Tags = new[] {"Settings"})]
        public async Task UpdateAsync(UpdateSettingInput input)
        {
            await _settingUiAppService.SetSettingValues(input.Values);
        }
    }
}
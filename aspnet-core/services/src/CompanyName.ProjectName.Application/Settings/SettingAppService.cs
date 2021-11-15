using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.SettingUi;
using EasyAbp.Abp.SettingUi.Authorization;
using EasyAbp.Abp.SettingUi.Dto;
using Microsoft.AspNetCore.Authorization;

namespace CompanyName.ProjectName.Settings
{
    [Authorize(Policy = SettingUiPermissions.ShowSettingPage)]
    public class SettingAppService : ProjectNameAppService, ISettingAppService
    {
        private readonly ISettingUiAppService _settingUiAppService;

        public SettingAppService(ISettingUiAppService settingUiAppService)
        {
            _settingUiAppService = settingUiAppService;
        }


        public async Task<List<SettingGroup>> GetAsync()
        {
            return await _settingUiAppService.GroupSettingDefinitions();
        }

        public async Task UpdateAsync(UpdateSettingInput input)
        {
            await _settingUiAppService.SetSettingValues(input.Values);
        }
    }
}
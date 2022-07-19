namespace Lion.AbpPro.Settings
{
    public interface ISettingAppService : IApplicationService
    {
        /// <summary>
        /// 获取setting信息
        /// </summary>
        /// <returns></returns>
        Task<List<SettingOutput>> GetAsync();
        
        /// <summary>
        /// 更新setting
        /// </summary>
        /// <returns></returns>
        Task UpdateAsync(UpdateSettingInput input);
    }
}
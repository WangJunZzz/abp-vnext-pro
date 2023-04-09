namespace Lion.AbpPro.LanguageManagement;

public interface ILocalizationHelper
{
    /// <summary>
    /// 资源是否有效
    /// </summary>
    Task<bool> IsValidResourceName(string resourceName);

    /// <summary>
    /// 获取所有资源名
    /// </summary>
    Task<List<string>> GetAllResourceName();

    IStringLocalizer GetLocalizer(LocalizationResource resource);
    IStringLocalizer GetLocalizer(string recourseName);
    LocalizationResource GetLocalizationResource(string resourceName);
}
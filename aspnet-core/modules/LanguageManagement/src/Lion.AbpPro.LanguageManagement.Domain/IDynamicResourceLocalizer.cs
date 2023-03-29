namespace Lion.AbpPro.LanguageManagement;

public interface IDynamicResourceLocalizer
{
    LocalizedString GetOrNull(LocalizationResourceBase resource, string cultureName, string name);

    void Fill(LocalizationResourceBase resource, string cultureName, Dictionary<string, LocalizedString> dictionary);
}
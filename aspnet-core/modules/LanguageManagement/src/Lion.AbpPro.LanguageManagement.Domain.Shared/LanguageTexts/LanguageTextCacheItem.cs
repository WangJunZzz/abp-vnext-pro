namespace Lion.AbpPro.LanguageManagement.LanguageTexts;

public class LanguageTextCacheItem
{
    public Dictionary<string, string> Dictionary { get; set; }

    public LanguageTextCacheItem()
    {
        this.Dictionary = new Dictionary<string, string>();
    }

    public static string CalculateCacheKey(string resourceName, string cultureName)
    {
        return resourceName + "_" + cultureName;
    }
}
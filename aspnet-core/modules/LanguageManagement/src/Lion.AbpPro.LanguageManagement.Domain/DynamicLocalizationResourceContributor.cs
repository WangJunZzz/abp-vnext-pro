namespace Lion.AbpPro.LanguageManagement;

public class DynamicLocalizationResourceContributor : ILocalizationResourceContributor
{
    private IDynamicResourceLocalizer DynamicResourceLocalizer;
    private LocalizationResourceBase Resource;

    public bool IsDynamic => true;

    public void Initialize(LocalizationResourceInitializationContext context)
    {
        Resource = context.Resource;
        DynamicResourceLocalizer = context.ServiceProvider.GetRequiredService<IDynamicResourceLocalizer>();
    }

    public LocalizedString GetOrNull(string cultureName, string name)
    {
        return DynamicResourceLocalizer.GetOrNull(Resource, cultureName, name);
    }

    public void Fill(string cultureName, Dictionary<string, LocalizedString> dictionary)
    {
        DynamicResourceLocalizer.Fill(Resource, cultureName, dictionary);
    }

    public Task FillAsync(string cultureName, Dictionary<string, LocalizedString> dictionary)
    {
        DynamicResourceLocalizer.Fill(Resource, cultureName, dictionary);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<string>> GetSupportedCulturesAsync()
    {
        // TODO 
        await Task.CompletedTask;
        return null;
    }
}
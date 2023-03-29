namespace Lion.AbpPro.LanguageManagement;

public static class AbpLocalizationOptionsExtensions
{
    public static AbpLocalizationOptions AddDynamicResource(this AbpLocalizationOptions localizationOptions)
    {
        localizationOptions.GlobalContributors.Add<DynamicLocalizationResourceContributor>();
        return localizationOptions;
    }
}
using Lion.AbpPro.LanguageManagement.Languages;
using Volo.Abp.SettingManagement;

namespace Lion.AbpPro.LanguageManagement.Data;

public class LanguageManagementDataSeedContributor : ITransientDependency, IDataSeedContributor
{
    private readonly ILanguageRepository _languageRepository;
    private readonly AbpLocalizationOptions _localizationOptions;
    private readonly IDataFilter<ISoftDelete> _softDeleteFilter;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ISettingManager _settingManager;
    private readonly ICurrentTenant _currentTenant;
    
    public LanguageManagementDataSeedContributor(
        ILanguageRepository languageRepository,
        IOptions<AbpLocalizationOptions> localizationOptions,
        IDataFilter<ISoftDelete> softDeleteFilter,
        IGuidGenerator guidGenerator,
        ISettingManager settingManager, 
        ICurrentTenant currentTenant)
    {
        _languageRepository = languageRepository;
        _softDeleteFilter = softDeleteFilter;
        _guidGenerator = guidGenerator;
        _settingManager = settingManager;
        _currentTenant = currentTenant;
        _localizationOptions = localizationOptions.Value;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            var defaultLanguage = await _settingManager.GetOrNullDefaultAsync(LanguageManagementConsts.SettingDefaultLanguage);
            using (_softDeleteFilter.Disable())
            {
                var languages = await _languageRepository.GetListAsync();
                foreach (var language in _localizationOptions.Languages)
                {
                    if (!languages.Any(e => e.CultureName == language.CultureName && e.UiCultureName == language.UiCultureName))
                    {
                        var isDefault = language.CultureName == defaultLanguage;
                        await _languageRepository.InsertAsync(
                            new Language(
                                _guidGenerator.Create(),
                                language.CultureName,
                                language.UiCultureName,
                                language.DisplayName,
                                language.FlagIcon,
                                true,
                                isDefault,
                                _currentTenant.Id));
                    }
                }
            } 
        }
       
    }
}
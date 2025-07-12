using Lion.AbpPro.BasicManagement.Features.Dtos;
using Volo.Abp.FeatureManagement;

namespace Lion.AbpPro.BasicManagement.Features;

[Authorize]
public class VoloFeatureAppService : BasicManagementAppService, IVoloFeatureAppService
{
    private readonly IFeatureAppService _featureAppService;
    protected FeatureManagementOptions Options { get; }
    protected IFeatureManager FeatureManager { get; }
    protected IFeatureDefinitionManager FeatureDefinitionManager { get; }

    public VoloFeatureAppService(IFeatureManager featureManager,
        IFeatureDefinitionManager featureDefinitionManager,
        IOptions<FeatureManagementOptions> options,
        IFeatureAppService featureAppService)
    {
        FeatureManager = featureManager;
        FeatureDefinitionManager = featureDefinitionManager;
        _featureAppService = featureAppService;
        Options = options.Value;
    }


    private async Task<GetFeatureListResultDto> GetAsync(string providerName, string providerKey)
    {
        var result = new GetFeatureListResultDto
        {
            Groups = new List<FeatureGroupDto>()
        };

        foreach (var group in await FeatureDefinitionManager.GetGroupsAsync())
        {
            var groupDto = CreateFeatureGroupDto(group);

            foreach (var featureDefinition in group.GetFeaturesWithChildren())
            {
                if (providerName == TenantFeatureValueProvider.ProviderName &&
                    CurrentTenant.Id == null &&
                    providerKey == null &&
                    !featureDefinition.IsAvailableToHost)
                {
                    continue;
                }

                var feature = await FeatureManager.GetOrNullWithProviderAsync(featureDefinition.Name, providerName, providerKey);
                groupDto.Features.Add(CreateFeatureDto(feature, featureDefinition));
            }

            SetFeatureDepth(groupDto.Features, providerName, providerKey);

            if (groupDto.Features.Any())
            {
                result.Groups.Add(groupDto);
            }
        }

        return result;
    }

    private void SetFeatureDepth(List<FeatureDto> features, string providerName, string providerKey, FeatureDto parentFeature = null, int depth = 0)
    {
        foreach (var feature in features)
        {
            if ((parentFeature == null && feature.ParentName == null) || (parentFeature != null && parentFeature.Name == feature.ParentName))
            {
                feature.Depth = depth;
                SetFeatureDepth(features, providerName, providerKey, feature, depth + 1);
            }
        }
    }

    private FeatureDto CreateFeatureDto(FeatureNameValueWithGrantedProvider featureNameValueWithGrantedProvider, FeatureDefinition featureDefinition)
    {
        return new FeatureDto
        {
            Name = featureDefinition.Name,
            DisplayName = featureDefinition.DisplayName?.Localize(StringLocalizerFactory),
            Description = featureDefinition.Description?.Localize(StringLocalizerFactory),

            ValueType = featureDefinition.ValueType,

            ParentName = featureDefinition.Parent?.Name,
            Value = featureNameValueWithGrantedProvider.Value,
            Provider = new FeatureProviderDto
            {
                Name = featureNameValueWithGrantedProvider.Provider?.Name,
                Key = featureNameValueWithGrantedProvider.Provider?.Key
            }
        };
    }

    private FeatureGroupDto CreateFeatureGroupDto(FeatureGroupDefinition groupDefinition)
    {
        return new FeatureGroupDto
        {
            Name = groupDefinition.Name,
            DisplayName = groupDefinition.DisplayName?.Localize(StringLocalizerFactory),
            Features = new List<FeatureDto>()
        };
    }

    public virtual async Task<GetFeatureListResultDto> GetAsync(GetFeatureListResultInput input)
    {
        var result = await GetAsync(input.ProviderName, input.ProviderKey);
        // 过滤自带的SettingManagement设置
        result.Groups = result.Groups.Where(e => e.Name != "SettingManagement").ToList();
        return result;
    }

    public virtual async Task UpdateAsync(UpdateFeatureInput input)
    {
        foreach (var feature in input.UpdateFeaturesDto.Features)
        {
            await FeatureManager.SetAsync(feature.Name, feature.Value, input.ProviderName, input.ProviderKey);
        }
    }

    public virtual async Task DeleteAsync(DeleteFeatureInput input)
    {
        await FeatureManager.DeleteAsync(input.ProviderName, input.ProviderKey);
    }
}
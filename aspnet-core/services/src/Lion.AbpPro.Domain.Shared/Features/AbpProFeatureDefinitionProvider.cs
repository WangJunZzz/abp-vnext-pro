using Volo.Abp.Features;
using Volo.Abp.Validation.StringValues;

namespace Lion.AbpPro.Features;

public class AbpProFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
       
        var group = context.AddGroup(AbpProFeatures.GroupName,L("Feature:TestGroup"));
        
        // ToggleStringValueType bool类型 前端渲染为checkbox
        group.AddFeature(AbpProFeatures.TestEnable,
            "true",
            L("Feature:TestEnable"),
            L("Feature:TestEnable"),
            new ToggleStringValueType());
        
        // ToggleStringValueType string类型 前端渲染为input
        group.AddFeature(AbpProFeatures.TestString,
            "输入需要设定的值",
            L("Feature:TestString"),
            L("Feature:TestString"),
            new FreeTextStringValueType());
        
        // todo SelectionStringValueType select标签待定
    }
    
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpProResource>(name);
    }
}
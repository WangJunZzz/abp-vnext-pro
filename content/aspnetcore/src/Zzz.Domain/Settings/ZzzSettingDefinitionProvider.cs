
using Volo.Abp.Localization;
using Volo.Abp.Settings;
using Zzz.Localization;

namespace Zzz.Settings
{
    public class ZzzSettingDefinitionProvider : SettingDefinitionProvider
    {
       

        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(ZzzSettings.MySetting1));
            context.Add(
            new SettingDefinition(
              name:"测试",
              defaultValue:"test",
              displayName: L("TestSettings")
            ).WithProviders(
              DefaultValueSettingValueProvider.ProviderName,
              GlobalSettingValueProvider.ProviderName)
      );;
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ZzzResource>(name);
        }
    }
}

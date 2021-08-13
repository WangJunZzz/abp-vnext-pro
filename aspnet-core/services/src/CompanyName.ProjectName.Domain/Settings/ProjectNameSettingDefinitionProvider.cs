using Volo.Abp.Settings;

namespace CompanyName.ProjectName.Settings
{
    public class ProjectNameSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(ProjectNameSettings.MySetting1));
        }
    }
}

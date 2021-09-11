using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace CompanyName.ProjectName
{
    [Dependency(ReplaceServices = true)]
    public class ProjectNameBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "ProjectName";
    }
}

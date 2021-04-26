using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CompanyNameProjectName
{
    [Dependency(ReplaceServices = true)]
    public class CompanyNameProjectNameBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "CompanyNameProjectName";
    }
}

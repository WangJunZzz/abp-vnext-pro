using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Lion.AbpPro.Web.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class WebAssemblyBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Web";
    }
}

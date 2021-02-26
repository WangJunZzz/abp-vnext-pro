using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Zzz
{
    [Dependency(ReplaceServices = true)]
    public class ZzzBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Zzz";
    }
}

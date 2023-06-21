using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Lion.AbpPro.EntityFrameworkCore.Tests;

[Dependency(ReplaceServices = true)]
public class TestsBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Tests";
}

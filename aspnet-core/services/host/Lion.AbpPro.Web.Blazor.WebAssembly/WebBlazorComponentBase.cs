
using Lion.AbpPro.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Lion.AbpPro.Web.Blazor
{
    public abstract class WebBlazorComponentBase : AbpComponentBase
    {
        protected WebBlazorComponentBase()
        {
            LocalizationResource = typeof(AbpProResource);
        }
    }
}

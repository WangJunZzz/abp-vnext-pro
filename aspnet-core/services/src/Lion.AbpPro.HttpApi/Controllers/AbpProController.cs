using Lion.AbpPro.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;

namespace Lion.AbpPro.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AbpProController : AbpController
    {
        protected AbpProController()
        {
            LocalizationResource = typeof(AbpProResource);
        }
    }
}
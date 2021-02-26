using Zzz.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Zzz.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ZzzController : AbpController
    {
        protected ZzzController()
        {
            LocalizationResource = typeof(ZzzResource);
        }
    }
}
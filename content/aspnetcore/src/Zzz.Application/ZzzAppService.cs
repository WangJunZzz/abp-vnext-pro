using System;
using System.Collections.Generic;
using System.Text;
using Zzz.Localization;
using Volo.Abp.Application.Services;

namespace Zzz
{
    /* Inherit your application services from this class.
     */
    public abstract class ZzzAppService : ApplicationService
    {
        protected ZzzAppService()
        {
            LocalizationResource = typeof(ZzzResource);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Lion.AbpPro.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.Localization;

namespace Lion.AbpPro
{
    /* Inherit your application services from this class.
     */
    public abstract class AbpProAppService : ApplicationService
    {
        protected AbpProAppService()
        {
            LocalizationResource = typeof(AbpProResource);
        }
    }
}

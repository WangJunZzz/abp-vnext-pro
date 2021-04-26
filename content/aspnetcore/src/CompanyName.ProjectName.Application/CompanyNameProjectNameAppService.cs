using System;
using System.Collections.Generic;
using System.Text;
using CompanyNameProjectName.Localization;
using Volo.Abp.Application.Services;

namespace CompanyNameProjectName
{
    /* Inherit your application services from this class.
     */
    public abstract class CompanyNameProjectNameAppService : ApplicationService
    {
        protected CompanyNameProjectNameAppService()
        {
            LocalizationResource = typeof(CompanyNameProjectNameResource);
        }
    }
}

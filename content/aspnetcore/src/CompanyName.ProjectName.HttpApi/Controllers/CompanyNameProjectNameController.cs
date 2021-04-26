using CompanyNameProjectName.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CompanyNameProjectName.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class CompanyNameProjectNameController : AbpController
    {
        protected CompanyNameProjectNameController()
        {
            LocalizationResource = typeof(CompanyNameProjectNameResource);
        }
    }
}
using CompanyName.ProjectName.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;

namespace CompanyName.ProjectName.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ProjectNameController : AbpController, IApplicationService
    {
        protected ProjectNameController()
        {
            LocalizationResource = typeof(ProjectNameResource);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using CompanyName.ProjectName.Localization;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName
{
    /* Inherit your application services from this class.
     */
    public abstract class ProjectNameAppService : ApplicationService
    {
        protected ProjectNameAppService()
        {
            LocalizationResource = typeof(ProjectNameResource);
        }
    }
}

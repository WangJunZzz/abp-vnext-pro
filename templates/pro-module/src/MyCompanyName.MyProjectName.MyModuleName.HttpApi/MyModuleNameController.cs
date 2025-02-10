using MyCompanyName.MyProjectName.MyModuleName.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MyCompanyName.MyProjectName.MyModuleName
{
    public abstract class MyModuleNameController : AbpController
    {
        protected MyModuleNameController()
        {
            LocalizationResource = typeof(MyModuleNameResource);
        }
    }
}

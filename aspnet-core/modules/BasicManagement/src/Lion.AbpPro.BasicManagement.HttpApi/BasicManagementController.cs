using Lion.AbpPro.BasicManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lion.AbpPro.BasicManagement;

public abstract class BasicManagementController : AbpController
{
    protected BasicManagementController()
    {
        LocalizationResource = typeof(BasicManagementResource);
    }
}

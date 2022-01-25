using Lion.AbpPro.FileManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lion.AbpPro.FileManagement;

public abstract class FileManagementController : AbpControllerBase
{
    protected FileManagementController()
    {
        LocalizationResource = typeof(FileManagementResource);
    }
}
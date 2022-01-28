using Lion.AbpPro.FileManagement.Localization;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.FileManagement;

public abstract class FileManagementAppService : ApplicationService
{
    protected FileManagementAppService()
    {
        LocalizationResource = typeof(FileManagementResource);
        ObjectMapperContext = typeof(FileManagementApplicationModule);
    }
}
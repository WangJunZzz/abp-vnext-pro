using Lion.AbpPro.BasicManagement.Localization;

namespace Lion.AbpPro.BasicManagement;

public abstract class BasicManagementAppService : ApplicationService
{
    protected BasicManagementAppService()
    {
        LocalizationResource = typeof(BasicManagementResource);
        ObjectMapperContext = typeof(BasicManagementApplicationModule);
    }
}

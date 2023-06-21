using Lion.AbpPro.EntityFrameworkCore.Tests.Localization;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.EntityFrameworkCore.Tests;

public abstract class TestsAppService : ApplicationService
{
    protected TestsAppService()
    {
        LocalizationResource = typeof(TestsResource);
        ObjectMapperContext = typeof(TestsApplicationModule);
    }
}

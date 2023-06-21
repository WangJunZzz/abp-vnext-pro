using Lion.AbpPro.EntityFrameworkCore.Tests.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lion.AbpPro.EntityFrameworkCore.Tests;

public abstract class TestsController : AbpControllerBase
{
    protected TestsController()
    {
        LocalizationResource = typeof(TestsResource);
    }
}

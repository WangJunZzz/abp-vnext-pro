using Lion.AbpPro.Localization;
using Microsoft.Extensions.Localization;
using Shouldly;
using Volo.Abp.Localization;
using Xunit;

namespace Lion.AbpPro
{
    public sealed class AbpProLocalizationTests : AbpProLocalizationTestBase
    {
        private readonly IStringLocalizer<AbpProLocalizationResource> _stringLocalizer;

        public AbpProLocalizationTests()
        {
            _stringLocalizer = GetRequiredService<IStringLocalizer<AbpProLocalizationResource>>();
        }

        [Fact]
        public void Test()
        {
            using (CultureHelper.Use("en"))
            {
                _stringLocalizer["Welcome"].Value
                    .ShouldBe("Welcome");
            }

            using (CultureHelper.Use("zh-Hans"))
            {
                _stringLocalizer["Welcome"].Value
                    .ShouldBe("欢迎");
            }
        }
    }
}
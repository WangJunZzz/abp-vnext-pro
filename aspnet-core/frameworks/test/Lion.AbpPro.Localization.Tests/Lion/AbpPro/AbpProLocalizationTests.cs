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
                _stringLocalizer["Welcome"].Value.ShouldBe("Welcome");
                _stringLocalizer[AbpProLocalizationErrorCodes.ErrorCode100001].Value.ShouldBe("The start page must be greater than or equal to 1");
                _stringLocalizer[AbpProLocalizationErrorCodes.ErrorCode100003,"Name"].Value.ShouldBe("Name can not be empty");
            }

            using (CultureHelper.Use("zh-Hans"))
            {
                _stringLocalizer["Welcome"].Value.ShouldBe("欢迎");
                _stringLocalizer[AbpProLocalizationErrorCodes.ErrorCode100001].Value.ShouldBe("起始页必须大于等于1");
                _stringLocalizer[AbpProLocalizationErrorCodes.ErrorCode100003,"Name"].Value.ShouldBe("Name不能为空");
            }
        }
    }
}
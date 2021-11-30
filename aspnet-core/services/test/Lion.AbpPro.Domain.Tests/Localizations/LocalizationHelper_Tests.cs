using System;
using Lion.AbpPro.Localization;
using Shouldly;
using Volo.Abp.Localization;
using Xunit;

namespace Lion.AbpPro.Localizations
{
    public class LocalizationHelper_Tests:AbpProDomainTestBase
    {
        [Fact]
        public void Test_LocalizationHelper_L_OK()
        {
            using (CultureHelper.Use("en"))
            {
                var enValue = LocalizationHelper.L["Welcome"];
                enValue.Value.ShouldBe("Welcome");
            }

            using (CultureHelper.Use("zh-Hans"))
            {
                 
                var enValue = LocalizationHelper.L["Welcome"];
                enValue.Value.ShouldBe("欢迎");
            }  
        }
    }
}
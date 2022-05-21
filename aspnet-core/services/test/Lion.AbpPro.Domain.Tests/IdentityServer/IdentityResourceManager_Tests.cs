using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace Lion.AbpPro.IdentityServer
{
    public class IdentityResourceManager_Tests : AbpProDomainTestBase
    {
        private readonly IdentityResourceManager _identityResourceManager;

        public IdentityResourceManager_Tests()
        {
            _identityResourceManager = GetRequiredService<IdentityResourceManager>();
        }


        [Fact]
        public async Task Shuold_GetListAsync_Ok()
        {
            var result = await _identityResourceManager.GetListAsync();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task Shuold_GetListAsync_Filter_Ok()
        {
            var result =
                await _identityResourceManager.GetListAsync(filter: "openid");
            result.FirstOrDefault()?.Name.ShouldBe("openid");
        }


        [Fact]
        public async Task Shuold_GetCountAsync_Ok()
        {
            var result = await _identityResourceManager.GetCountAsync();
            result.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Shuold_GetCountAsync_Filter_Ok()
        {
            var result = await _identityResourceManager.GetCountAsync(filter: "openid");
            result.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Shuold_CreateAsync_Ok()
        {
            var result = await _identityResourceManager.CreateAsync("Create_Identity", "单元测试",
                "desc", true, false,
                false,
                false);
            result.Name.ShouldBe("Create_Identity");
            result.Enabled.ShouldBeTrue();
            result.ShowInDiscoveryDocument.ShouldBeFalse();
        }
        
        [Fact]
        public async Task Shuold_CreateAsync_Name_Repetition_Exception()
        {
            (await Should.ThrowAsync<BusinessException>(async () =>
            {
                var result = await _identityResourceManager.CreateAsync("openid", "单元测试",
                    "desc", true, false,
                    false,
                    false);
            })).Code.ShouldBe(AbpProDomainErrorCodes.IdentityResourceExist);
        }
        
        
        [Fact]
        public async Task Shuold_UpdateAsync_Ok()
        {
            var result = await _identityResourceManager.UpdateAsync("openid", "单元测试",
                "desc", false, false,
                false,
                false);
            result.DisplayName.ShouldBe("单元测试");
            result.Enabled.ShouldBeFalse();
        }
        
        [Fact]
        public async Task Shuold_DeleteAsync_Ok()
        {
            await _identityResourceManager.DeleteAsync(Guid.NewGuid());
        }
    }
}
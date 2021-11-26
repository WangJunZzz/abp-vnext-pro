using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace Lion.AbpPro.IdentityServer
{
    public class IdenityServerClientManager_Tests : AbpProDomainTestBase
    {
        private readonly IdenityServerClientManager _idenityServerClientManager;

        public IdenityServerClientManager_Tests()
        {
            _idenityServerClientManager = GetRequiredService<IdenityServerClientManager>();
        }

        [Fact]
        public async Task Shuold_GetListAsync_Ok()
        {
            var result = await _idenityServerClientManager.GetListAsync();
            result.ShouldNotBeNull();
            result.FirstOrDefault()?.ClientName.ShouldBe("Test_Client");
        }

        [Fact]
        public async Task Shuold_GetListAsync_Filter_Ok()
        {
            var result =
                await _idenityServerClientManager.GetListAsync(filter: "Test_Client");
            result.ShouldNotBeNull();
            result.FirstOrDefault()?.ClientName.ShouldBe("Test_Client");
        }


        [Fact]
        public async Task Shuold_GetCountAsync_Ok()
        {
            var result = await _idenityServerClientManager.GetCountAsync();
            result.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Shuold_GetCountAsync_Filter_Ok()
        {
            var result = await _idenityServerClientManager.GetCountAsync(filter: "Test_Client");
            result.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Shuold_CreateAsync_Ok()
        {
            var result =
                await _idenityServerClientManager.CreateAsync("Xunit", "XunitName", "desc", "test");
            result.ClientName.ShouldBe("XunitName");
            result.Enabled.ShouldBeTrue();
        }

        [Fact]
        public async Task Shuold_CreateAsync_Name_Repetition_Exception()
        {
            (await Should.ThrowAsync<UserFriendlyException>(async () =>
            {
                var result =
                    await _idenityServerClientManager.CreateAsync("Test_Client", "XunitName",
                        "desc", "test");
            })).Message.ShouldBe("当前ClientId已存在");
        }

        [Fact]
        public async Task Shuold_UpdateBasicDataAsync_Ok()
        {
            var result = await _idenityServerClientManager.UpdateBasicDataAsync(
                "Test_Client",
                "单元测试",
                "desc",
                "clientUrl",
                "logoUrl",
                false,
                "http",
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                "logoutUrl",
                false,
                "backLogoutUrl",
                false,
                false,
                300,
                "unit",
                3500,
                3400,
                3300,
                3200,
                3100,
                false,
                3000,
                1,
                false,
                false,
                false,
                "client",
                "salt",
                2900,
                "user",
                2600,
                2800,
                "123456",
                "123456",
                "sc"
            );
            result.Description.ShouldBe("desc");
            result.Enabled.ShouldBeFalse();
            result.AccessTokenLifetime.ShouldBe(3500);
            result.AuthorizationCodeLifetime.ShouldBe(3400);
            result.IdentityTokenLifetime.ShouldBe(300);
            result.RefreshTokenExpiration.ShouldBe(3000);
        }

        [Fact]
        public async Task Shuold_UpdateScopesAsync_Ok()
        {
            var result =
                await _idenityServerClientManager.UpdateScopesAsync("Test_Client",
                    new List<string>() { "001","002" });
           result.AllowedScopes.Count.ShouldBe(2);
        }
        
        [Fact]
        public async Task Shuold_AddRedirectUriAsync_Ok()
        {
            var result =
                await _idenityServerClientManager.AddRedirectUriAsync("Test_Client","doc.cncore.club");
            result.RedirectUris.FirstOrDefault(e => e.RedirectUri == "doc.cncore.club")
                .ShouldNotBeNull();
        }
        
        [Fact]
        public async Task Shuold_RemoveRedirectUriAsync_Ok()
        {
            await WithUnitOfWorkAsync(async () => { await _idenityServerClientManager.AddRedirectUriAsync("Test_Client","doc.cncore.club");; });
            
            var result =
                await _idenityServerClientManager.RemoveRedirectUriAsync("Test_Client","doc.cncore.club");
            result.RedirectUris.FirstOrDefault(e => e.RedirectUri == "doc.cncore.club")
                .ShouldBeNull();
        }
        
        
        [Fact]
        public async Task Shuold_AddLogoutRedirectUriAsync_Ok()
        {
            var result =
                await _idenityServerClientManager.AddLogoutRedirectUriAsync("Test_Client","doc.cncore.club");
            result.PostLogoutRedirectUris.Any(e=>e.PostLogoutRedirectUri=="doc.cncore.club").ShouldBeTrue();
        }
        
        [Fact]
        public async Task Shuold_RemoveLogoutRedirectUriAsync_Ok()
        {
            await WithUnitOfWorkAsync(async () => { await _idenityServerClientManager.AddLogoutRedirectUriAsync("Test_Client","doc.cncore.club");; });
            
            var result =
                await _idenityServerClientManager.RemoveLogoutRedirectUriAsync("Test_Client","doc.cncore.club");
            result.PostLogoutRedirectUris.Any(e=>e.PostLogoutRedirectUri=="doc.cncore.club").ShouldBeFalse();
        }
        
        [Fact]
        public async Task Shuold_AddCorsAsync_Ok()
        {
            var result =
                await _idenityServerClientManager.AddCorsAsync("Test_Client","doc.cncore.club");
            result.AllowedCorsOrigins.FirstOrDefault(e => e.Origin == "doc.cncore.club")
                .ShouldNotBeNull();
        }
        
        [Fact]
        public async Task Shuold_RemoveCorsAsync_Ok()
        {
            await WithUnitOfWorkAsync(async () => { await _idenityServerClientManager.AddCorsAsync("Test_Client","doc.cncore.club");; });
            
            var result =
                await _idenityServerClientManager.RemoveCorsAsync("Test_Client","doc.cncore.club");
            result.AllowedCorsOrigins.FirstOrDefault(e => e.Origin == "doc.cncore.club")
                .ShouldBeNull();
        }
        
        [Fact]
        public async Task Shuold_EnabledAsync_Ok()
        {
            var result =
                await _idenityServerClientManager.EnabledAsync("Test_Client",false);
            result.Enabled.ShouldBeFalse();
        }
    }
}
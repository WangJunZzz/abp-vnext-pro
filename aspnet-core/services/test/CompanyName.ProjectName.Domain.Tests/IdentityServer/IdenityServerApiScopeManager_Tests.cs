using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace CompanyName.ProjectName.IdentityServer
{
    public class IdenityServerApiScopeManager_Tests : ProjectNameDomainTestBase
    {
        private readonly IdenityServerApiScopeManager _idenityServerApiScopeManager;

        public IdenityServerApiScopeManager_Tests()
        {
            _idenityServerApiScopeManager = GetRequiredService<IdenityServerApiScopeManager>();
        }

        [Fact]
        public async Task Shuold_GetListAsync_Ok()
        {
            var result = await _idenityServerApiScopeManager.GetListAsync();
            result.Count.ShouldBe(1);
            result.FirstOrDefault()?.Name.ShouldBe("ApiScope_Test");
        }

        [Fact]
        public async Task Shuold_GetListAsync_Filter_Ok()
        {
            var result =
                await _idenityServerApiScopeManager.GetListAsync(filter: "ApiScope_Test");
            result.Count.ShouldBe(1);
            result.FirstOrDefault()?.Name.ShouldBe("ApiScope_Test");
        }

        [Fact]
        public async Task Shuold_GetCountAsync_Ok()
        {
            var result = await _idenityServerApiScopeManager.GetCountAsync();
            result.ShouldBe(1);
        }

        [Fact]
        public async Task Shuold_GetCountAsync_Filter_Ok()
        {
            var result = await _idenityServerApiScopeManager.GetCountAsync(filter: "ApiScope_Test");
            result.ShouldBe(1);
        }

        [Fact]
        public async Task Shuold_CreateAsync_Ok()
        {
            var result = await _idenityServerApiScopeManager.CreateAsync(
                "Create_ApiScope", "单元测试创建", "Xunit", true, false, false, true);
            result.Name.ShouldBe("Create_ApiScope");
        }
        
        [Fact]
        public async Task Shuold_CreateAsync_Name_Repetition_Exception()
        {
            (await Should.ThrowAsync<UserFriendlyException>(async () =>
            {
                var result = await _idenityServerApiScopeManager.CreateAsync(
                    "ApiScope_Test", "单元测试创建", "Xunit", true, false, false, true);
            })).Message.ShouldBe("ApiScope_Test已存在");
        }
        
        // [Fact]
        // public async Task Shuold_UpdateAsync_Ok()
        // {
        //     var result = await _idenityServerApiScopeManager.UpdateAsync("ApiScope_Test",
        //         "Update", "Update_Desc", true, true, true,
        //         true);
        //     result.DisplayName.ShouldBe("Update");
        //     result.Enabled.ShouldBeTrue();
        // }
        
        [Fact]
        public async Task Shuold_DeleteAsync_Ok()
        {
            await _idenityServerApiScopeManager.DeleteAsync(Guid.NewGuid());
        }

    }
}
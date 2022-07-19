namespace Lion.AbpPro.IdentityServer
{
    public class IdenityServerApiResourceManager_Tests : AbpProDomainTestBase
    {
        private readonly IdenityServerApiResourceManager _idenityServerApiResourceManager;

        public IdenityServerApiResourceManager_Tests()
        {
            _idenityServerApiResourceManager =
                GetRequiredService<IdenityServerApiResourceManager>();
        }

        [Fact]
        public async Task Shuold_GetListAsync_Ok()
        {
            var result = await _idenityServerApiResourceManager.GetListAsync();
            result.Count.ShouldBe(1);
            result.FirstOrDefault()?.Name.ShouldBe("ApiResource_Test");
        }

        [Fact]
        public async Task Shuold_GetListAsync_Filter_Ok()
        {
            var result =
                await _idenityServerApiResourceManager.GetListAsync(filter: "ApiResource_Test");
            result.Count.ShouldBe(1);
            result.FirstOrDefault()?.Name.ShouldBe("ApiResource_Test");
        }


        [Fact]
        public async Task Shuold_GetCountAsync_Ok()
        {
            var result = await _idenityServerApiResourceManager.GetCountAsync();
            result.ShouldBe(1);
        }

        [Fact]
        public async Task Shuold_GetCountAsync_Filter_Ok()
        {
            var result = await _idenityServerApiResourceManager.GetCountAsync(filter:"ApiResource_Test");
            result.ShouldBe(1);
        }

        [Fact]
        public async Task Shuold_CreateAsync_Ok()
        {
            var result = await _idenityServerApiResourceManager.CreateAsync(Guid.NewGuid(),
                "Create_ApiResource", "单元测试创建", "Xunit", true, "", false, "1q2w3E*");
            result.Name.ShouldBe("Create_ApiResource");
        }

        [Fact]
        public async Task Shuold_CreateAsync_Name_Repetition_Exception()
        {
            (await Should.ThrowAsync<BusinessException>(async () =>
            {
                var result = await _idenityServerApiResourceManager.CreateAsync(Guid.NewGuid(),
                    "ApiResource_Test", "单元测试创建", "Xunit", true, "", false, "1q2w3E*");
            })).Code.ShouldBe(AbpProDomainErrorCodes.ApiResourceExist);
        }


        [Fact]
        public async Task Shuold_UpdateAsync_Ok()
        {
            var result = await _idenityServerApiResourceManager.UpdateAsync("ApiResource_Test",
                "Update", "Update_Desc", false, "", true,
                "123456", new List<string>());
            result.Name.ShouldBe("ApiResource_Test");
            result.DisplayName.ShouldBe("Update");
            result.Enabled.ShouldBeFalse();
        }

        [Fact]
        public async Task Shuold_DeleteAsync_Ok()
        {
            await _idenityServerApiResourceManager.DeleteAsync(Guid.NewGuid());
        }
    }
}
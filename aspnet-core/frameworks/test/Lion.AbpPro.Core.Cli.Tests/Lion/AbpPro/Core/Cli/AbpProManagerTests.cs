using Lion.AbpPro.Cli.Github;
using Shouldly;
using Xunit;

namespace Lion.AbpPro.Core.Cli;

public sealed class AbpProManagerTests : AbpProCoreCliTestBase
{
    private readonly IAbpProManager _abpProManager;

    public AbpProManagerTests()
    {
        _abpProManager = GetRequiredService<IAbpProManager>();
    }

    [Fact]
    public async Task GetLatestSourceCodeVersionAsync()
    {
       var result= await _abpProManager.GetLatestSourceCodeVersionAsync();
       result.ShouldBe("7.2.2.3");
    }
    
    [Fact]
    public async Task CheckSourceCodeVersionAsync()
    {
        var result= await _abpProManager.CheckSourceCodeVersionAsync("7.2.2.3");
        result.ShouldBe(true);
        
        var result1= await _abpProManager.CheckSourceCodeVersionAsync("1.2.2.3");
        result1.ShouldBe(false);
    }
}
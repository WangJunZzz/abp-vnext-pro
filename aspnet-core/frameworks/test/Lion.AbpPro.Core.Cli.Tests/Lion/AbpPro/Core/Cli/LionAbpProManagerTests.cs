using Lion.AbpPro.Cli.Github;
using Shouldly;
using Xunit;

namespace Lion.AbpPro.Core.Cli;

public sealed class LionAbpProManagerTests : LionAbpProCoreCliTestBase
{
    private readonly ILionAbpProManager _lionAbpProManager;

    public LionAbpProManagerTests()
    {
        _lionAbpProManager = GetRequiredService<ILionAbpProManager>();
    }

    [Fact]
    public async Task GetLatestSourceCodeVersionAsync()
    {
       var result= await _lionAbpProManager.GetLatestSourceCodeVersionAsync();
       result.ShouldBe("7.2.2.3");
    }
    
    [Fact]
    public async Task CheckSourceCodeVersionAsync()
    {
        var result= await _lionAbpProManager.CheckSourceCodeVersionAsync("7.2.2.3");
        result.ShouldBe(true);
        
        var result1= await _lionAbpProManager.CheckSourceCodeVersionAsync("1.2.2.3");
        result1.ShouldBe(false);
    }
}
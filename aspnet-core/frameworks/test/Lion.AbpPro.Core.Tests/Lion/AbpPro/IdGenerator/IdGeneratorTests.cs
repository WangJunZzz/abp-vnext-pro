using Lion.AbpPro.Core;
using Xunit.Abstractions;
using Yitter.IdGenerator;

namespace Lion.AbpPro.IdGenerator;

public class IdGeneratorTests : AbpProTestBase
{
    private readonly ITestOutputHelper _testOutputHelper;

    public IdGeneratorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void NextId()
    {
        for (int j = 0; j < 1000; j++)
        {
            _testOutputHelper.WriteLine(YitIdHelper.NextId().ToString());
        }
    }
}
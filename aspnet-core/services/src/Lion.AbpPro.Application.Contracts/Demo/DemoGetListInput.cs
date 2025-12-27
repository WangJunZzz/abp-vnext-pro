using Lion.AbpPro.Ddd.Application.Contracts;

namespace Lion.AbpPro.Demo;

public class DemoGetListInput : AbpProPagedInput
{
    public string Name { get; set; }

    public string Description { get; set; }
}
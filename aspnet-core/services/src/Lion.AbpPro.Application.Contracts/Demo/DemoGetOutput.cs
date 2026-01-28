using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.Demo;

public class DemoGetOutput : EntityDto<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreationTime { get; set; }
}
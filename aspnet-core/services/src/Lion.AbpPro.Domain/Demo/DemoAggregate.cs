using Volo.Abp.Domain.Entities.Auditing;

namespace Lion.AbpPro.Demo;

/// <summary>
/// 演示聚合
/// </summary>
public class DemoAggregate : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
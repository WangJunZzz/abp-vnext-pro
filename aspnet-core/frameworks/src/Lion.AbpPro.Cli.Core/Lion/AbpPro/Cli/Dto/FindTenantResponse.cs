namespace Lion.AbpPro.Cli.Dto;

public class FindTenantResponse
{
    public Guid? TenantId { get; set; }
    
    public bool Success { get; set; }
}
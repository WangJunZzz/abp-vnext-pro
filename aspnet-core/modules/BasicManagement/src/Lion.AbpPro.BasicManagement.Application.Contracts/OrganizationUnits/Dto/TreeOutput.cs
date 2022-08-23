namespace Lion.AbpPro.BasicManagement.OrganizationUnits.Dto;

public class TreeOutput
{
    public string Title { get; set; }
    
    public Guid Key { get; set; }
    
    public List<TreeOutput> Children { get; set; }
}
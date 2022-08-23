namespace Lion.AbpPro.BasicManagement.Roles.Dtos
{
    public class PermissionOutput {
        public List<string> Grants { get; set; } = new List<string>();

        public List<string> AllGrants { get; set; } = new List<string>();

        public List<PermissionTreeDto> Permissions { get; set; } = new List<PermissionTreeDto>();
    }

    public class PermissionTreeDto
    {
        public string Title { get; set; }

        public string Key { get; set; }

        public List<PermissionTreeDto> Children { get; set; }=new List<PermissionTreeDto>();
    }
}
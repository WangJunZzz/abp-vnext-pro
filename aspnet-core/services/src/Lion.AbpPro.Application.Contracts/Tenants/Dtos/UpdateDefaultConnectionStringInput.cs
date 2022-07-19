namespace Lion.AbpPro.Tenants.Dtos
{
    public class UpdateConnectionStringInput
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "连接字符串不能为空")] public string ConnectionString { get; set; }
    }
}
namespace Lion.AbpPro.BasicManagement.Users.Dtos
{
    public class LoginOutput
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}

namespace Lion.AbpPro.BasicManagement.Users.Dtos
{
    public class LoginStsResponse
    {
        public string name { get; set; }

        public string preferred_username { get; set; }

        public string family_name { get; set; }

        public string email { get; set; }

        public string given_name { get; set; }

        public string avatar { get; set; }

        public string sub { get; set; }
        
        public Guid? tenantId { get; set; }
    }
}

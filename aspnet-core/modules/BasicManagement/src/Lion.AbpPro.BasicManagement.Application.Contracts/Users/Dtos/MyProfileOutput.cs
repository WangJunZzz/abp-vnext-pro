namespace Lion.AbpPro.BasicManagement.Users.Dtos;

public class MyProfileOutput
{
    public Guid? TenantId { get; set; }

    public string UserName { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public bool TwoFactorEnabled { get; set; }
}
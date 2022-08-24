namespace Lion.AbpPro.BasicManagement.Users.Dtos
{
    public class LockUserInput
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public bool Locked { get; set; }
    }
}

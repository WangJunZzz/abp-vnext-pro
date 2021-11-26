using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lion.AbpPro.Users.Dtos
{
    public class LockUserInput
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public bool Locked { get; set; }
    }
}

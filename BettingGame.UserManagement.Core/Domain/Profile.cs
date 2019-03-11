using System;
using System.ComponentModel.DataAnnotations;

namespace BettingGame.UserManagement.Core.Domain
{
    public class Profile
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        public Guid Id { get; set; }

        public bool IsAdmin { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nickname { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.UserManagement.Core.Features.Registration
{
    public class RegisterUserCommand : ICommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}

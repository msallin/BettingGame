using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.UserManagement.Core.Features.SignIn
{
    public class SignInValidQuery : IQuery<string>
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}

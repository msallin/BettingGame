using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.UserManagement.Core.Features.UserProfile
{
    public class UpdateUserProfileCommand : ICommand
    {
        [Required]
        [MaxLength(100)]
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
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BettingGame.UserManagement.Core.Domain
{
    public class User : Profile
    {
        [Required]
        public string PasswordHash { get; set; }

        public IList<string> Roles { get; set; }
    }
}

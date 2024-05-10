using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FribergHomes.Client.DTOs
{
    public class LoginRealtorDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}

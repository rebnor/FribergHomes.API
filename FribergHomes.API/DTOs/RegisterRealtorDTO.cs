using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FribergHomes.API.DTOs
{
    public class RegisterRealtorDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        public int AgencyId { get; set; }
    }
}

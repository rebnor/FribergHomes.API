using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FribergHomes.API.Models
{
    // Author: Sanna 
    
    public class Realtor : IdentityUser
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        //[Required, EmailAddress]
        //public string Email { get; set; }

        //[Required, Display(Name = "Telefonnummer")]
        //public string PhoneNumber { get; set; }
        
        public string? Picture { get; set; }        // Test

        [Required, Display(Name = "Mäklarbyrå")]
        public Agency Agency { get; set; }



    }
}

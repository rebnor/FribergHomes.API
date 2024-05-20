using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FribergHomes.API.Models
{
    // Author: Sanna 
    
    public class Realtor : IdentityUser
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string? Picture { get; set; }

        [Required, Display(Name = "Mäklarbyrå")]
        public Agency Agency { get; set; }



    }
}

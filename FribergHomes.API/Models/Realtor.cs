using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
    //Author: Sanna 
    public class Realtor
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public Agency Agency { get; set; }       
        public List<House>? Houses { get; set; }


    }
}

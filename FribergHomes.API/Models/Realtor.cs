using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FribergHomes.API.Models
{
    //Author: Sanna 
    public class Realtor
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name="Förnamn") ]
        public string FirstName { get; set; }
        [Required, Display(Name = "Efternamn")]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required, Display(Name = "Mäklarbyrå")]
        public Agency Agency { get; set; }

        [InverseProperty("Realtor")]
        public List<SalesObject>? SalesObjects { get; set; }


    }
}

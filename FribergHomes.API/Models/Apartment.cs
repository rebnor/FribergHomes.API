using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
    //Creator: Tobias Ledin

    public class Apartment : SalesObject
    {
        [Required]
        public int Level { get; set; }

        [Required]
        public bool Lift { get; set; }

        protected Apartment(int price) : base(price)
        {

        }
    }
}

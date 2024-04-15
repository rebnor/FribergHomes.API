using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
     /* Class for Apartment (inherits from SalesObject).
      * Author: Tobias 2024-04-15
      */

    public class Apartment : SalesObject
    {
        [Required]
        public int Level { get; set; }

        [Required]
        public bool Lift { get; set; }

        [Required]
        public double MonthlyFee { get; set; }

        protected Apartment(int price) : base(price)
        {

        }
    }
}

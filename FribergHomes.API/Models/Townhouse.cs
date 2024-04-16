using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
     /* Class for Townhouse (inherits from SalesObject).
      * Author: Tobias 2024-04-15
      */

    public class Townhouse : SalesObject
    {
        [Required]
        public double MonthlyFee { get; set; }

    }
}

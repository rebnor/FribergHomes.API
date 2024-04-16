using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
     /* Class for SalesObject which forms the base class for all real estate objects.
      * Author: Tobias 2024-04-15
      */

    public class House : SalesObject
    {
        [Required]
        public double PlotArea { get; set; }
        [Required]
        public double YearlyCost { get; set; }

    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
     /* Class for SalesObject which forms the base class for all real estate objects.
      * Author: Tobias 2024-04-15
      * Revised: Tobias 2024-04-17 - Flattened model structure.
      * Integrated all derived class properties into SalesObject. Added Display-names and updated required attributes.
      * Revised: Tobias 2024-04-23 - Updated property attributes with introduction of SalesObjectDTO.
      * Added contructor to set Listing price at object creation based on Price (for comparison reasons).
      * Removed field values.
      */

    public class SalesObject
    {
        public SalesObject()
        {
            ListingPrice = CurrentPrice;
        }

        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string CreatorName { get; set; } = string.Empty;

        public DateTime? ChangeDate { get; set; }

        public string? ChangeName { get; set; } = string.Empty;

        [Required]
        public string Adress { get; set; } = string.Empty;

        [Required]
        public int Rooms { get; set; }

        [Required]
        public double LivingArea { get; set; }

        public double? AncillaryArea { get; set; }

        public double? PlotArea { get; set; }

        public double? YearlyCost { get; set; }

        public double? MonthlyFee { get; set; }

        public int? Level { get; set; }

        public bool? Lift { get; set; }

        // Original price (for percent change comparison if object price changes).
        // Only gets set once when object is created.
        public int ListingPrice { get; init; }

        [Required]
        public int CurrentPrice { get; set; }

        [Required]
        public string ObjectDescription { get; set; } = string.Empty;

        [Required]
        public int BuildYear { get; set; }

        public List<string> ImageLinks { get; set; } = [];

        public List<DateTime> ViewingDates { get; set; } = [];

        public Realtor? Realtor { get; set; }

        public County? County { get; set; }

        public Category? Category { get; set; }


    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
     /* Model for SalesObject which reprents real-estate properties for sale.
      * Author: Tobias 2024-04-15 
      */

    public class SalesObject
    {

        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string CreatorName { get; set; } = string.Empty;

        public DateTime? ChangeDate { get; set; }

        public string? ChangeName { get; set; } = string.Empty;

        public string Adress { get; set; } = string.Empty;

        public int Rooms { get; set; }

        public double LivingArea { get; set; }

        public double? AncillaryArea { get; set; }

        public double? PlotArea { get; set; }

        public double? YearlyCost { get; set; }

        public double? MonthlyFee { get; set; }

        public int? Level { get; set; }

        public bool? Lift { get; set; }

        public int ListingPrice { get; set; }

        public int CurrentPrice { get; set; }

        public string ObjectDescription { get; set; } = string.Empty;

        public int BuildYear { get; set; }

        public List<string> ImageLinks { get; set; } = [];

        public List<DateTime> ViewingDates { get; set; } = [];

        public Realtor? Realtor { get; set; }

        public County? County { get; set; }

        public Category? Category { get; set; }


    }
}

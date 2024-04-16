using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
     /* Class for SalesObject which forms the base class for all real estate objects.
      * Author: Tobias 2024-04-15
      */

    public class SalesObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        public string CreatorName { get; set; } = string.Empty;

        public DateTime ChangeDate { get; set; }

        public string ChangeName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s-\d]+$", ErrorMessage = "Adress får enbart innehålla a-ö, 0-9, bindestreck och mellanslag")]
        public string Adress { get; set; } = string.Empty;

        [Required]
        [DisplayName("Boarea")]
        public double LivingArea { get; set; }

        [Required]
        [DisplayName("Biarea")]
        public double AncillaryArea { get; set; }

        // Only gets set once (when created).
        [Required]
        public int ListingPrice { get; init; }


        private int _currentPrice;

        public int CurrentPrice
        { 
            get => _currentPrice;
            set
            {
                if (value != _currentPrice)
                {
                    _currentPrice = value;

                }
                else if (CurrentPrice == 0)
                {
                    _currentPrice = ListingPrice;
                }
            }
        }  

        public string ObjectDescription { get; set; } = string.Empty;

        [Required]
        public int Rooms { get; set; }

        [Required]
        public int BuildYear { get; set; }

        public List<string> ImageLinks { get; set; } = [];

        public List<DateTime> ViewingDates { get; set; } = [];

        [Required]
        public Realtor? Realtor { get; set; }

        [Required]
        public County? County { get; set; }

        [Required]
        public Category? Category { get; set; }

    }
}

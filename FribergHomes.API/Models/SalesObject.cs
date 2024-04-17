using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
     /* Class for SalesObject which forms the base class for all real estate objects.
      * Author: Tobias 2024-04-15
      * Revised: Tobias 2024-04-17 - Flattened model structure.
      * Integrated all derived class properties into SalesObject. Added Display-names and updated required attributes.
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
        [DisplayName("Rum")]
        public int Rooms { get; set; }

        [Required]
        [DisplayName("Boarea")]
        public double LivingArea { get; set; }

        [DisplayName("Biarea")]
        public double AncillaryArea { get; set; }

        [DisplayName("Tomtarea")]
        public double PlotArea { get; set; }

        [DisplayName("Driftkostnad")]
        public double YearlyCost { get; set; }

        [DisplayName("Månadsavgift")]
        public double MonthlyFee { get; set; }

        [DisplayName("Våning")]
        public int Level { get; set; }

        [DisplayName("Hiss")]
        public bool Lift { get; set; }

        // Only gets set once when object is created.
        [Required]
        public int ListingPrice { get; init; }


        private int _currentPrice;

        [DisplayName("Pris")]
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

        [Required]
        [DisplayName("Objektbeskrivning")]
        public string ObjectDescription { get; set; } = string.Empty;

        [DisplayName("Byggnadsår")]
        public int BuildYear { get; set; }

        public List<string> ImageLinks { get; set; } = [];

        [DisplayName("Visningsdatum")]
        public List<DateTime> ViewingDates { get; set; } = [];

        [DisplayName("Mäklare")]
        public Realtor? Realtor { get; set; }

        [DisplayName("Kommun")]
        public County? County { get; set; }

        [DisplayName("Bostadskategori")]
        public Category? Category { get; set; }

    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
    //Creator: Tobias Ledin

    public class SalesObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        public int CreatorId { get; set; }          // int ID eller Realtor?

        public DateTime ChangeDate { get; set; }

        public int ChangeId { get; set; }           // int ID eller Realtor?

        [Required]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s-\d]+$", ErrorMessage = "Adress får enbart innehålla a-ö, 0-9, bindestreck och mellanslag")]
        public string Adress { get; set; } = string.Empty;       // Bryt upp i gatunamn, nummer, våning ?

        [Required]
        [DisplayName("Boarea")]
        public double LivingArea { get; set; }

        [Required]
        [DisplayName("Biarea")]
        public double AncillaryArea { get; set; }

        [Required]
        public int OriginalPrice { get; init; }


        private int _actualPrice;

        public int ActualPrice
        { 
            get => _actualPrice;
            set
            {
                if(value != _actualPrice)
                {
                    _actualPrice = value;
                    PriceChangePercent = CalculatePercentChange();
                }
            }
        }  

        public double PriceChangePercent { get; set; }

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


        protected SalesObject(int price)
        {
            OriginalPrice = price;
            _actualPrice = price;
        }


        private double CalculatePercentChange()
        {
            if (OriginalPrice == 0)
            {
                return 0; 
            }
            return (double)(ActualPrice - OriginalPrice) / OriginalPrice * 100;
        }
    }
}

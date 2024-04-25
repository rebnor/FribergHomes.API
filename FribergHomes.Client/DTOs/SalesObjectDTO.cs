using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FribergHomes.Client.DTOs
{
    /* SalesObjectDTO that will be used in client-API requests and responses.
     * Author: Tobias 2024-04-23
     * @ Update: Switched from County-object to CountyName string / Reb 2024-05-25
     */

    public class SalesObjectDTO
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string CreatorName { get; set; } = string.Empty;

        public DateTime? ChangeDate { get; set; }

        public string? ChangeName { get; set; } = string.Empty;

        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s-\d]+$", ErrorMessage = "Adress får enbart innehålla a-ö, 0-9, bindestreck och mellanslag")]
        public string Adress { get; set; } = string.Empty;

        [DisplayName("Rum")]
        [Length(1, 50, ErrorMessage = "Antal rum måste vara minst 1 och högst 50")]
        public int Rooms { get; set; }

        [DisplayName("Boarea")]
        [Length(10, 1000, ErrorMessage = "Bostadsarea måste vara minst 10 och högst 1000m2")]
        public double LivingArea { get; set; }

        [DisplayName("Biarea")]
        [MaxLength(1000, ErrorMessage = "Biarea får ej överstiga 1000m2")]
        public double AncillaryArea { get; set; }

        [DisplayName("Tomtarea")]
        [MaxLength(1000000, ErrorMessage = "Tomtarea får ej överstiga 1000000m2")]
        public double? PlotArea { get; set; }

        [DisplayName("Driftkostnad")]
        public double? YearlyCost { get; set; }

        [DisplayName("Månadsavgift")]
        public double? MonthlyFee { get; set; }

        [DisplayName("Våning")]
        [MaxLength(100, ErrorMessage = "Våning får ej överstiga 100")]
        public int? Level { get; set; }

        [DisplayName("Hiss")]
        public bool? Lift { get; set; }

        public int ListingPrice { get; init; }

        [DisplayName("Pris")]
        public int CurrentPrice { get; set; }

        [DisplayName("Objektbeskrivning")]
        public string ObjectDescription { get; set; } = string.Empty;

        [DisplayName("Byggnadsår")]
        [MinLength(1800, ErrorMessage = "Byggnadsår får ej vara tidigare än 1800")]
        public int BuildYear { get; set; }

        public List<string> ImageLinks { get; set; } = [];

        [DisplayName("Visningsdatum")]
        public List<DateTime> ViewingDates { get; set; } = [];

        public int RealtorId { get; set; }

        [DisplayName("Mäklare")]
        public string RealtorName { get; set; } = string.Empty;

        public string RealtorEmail { get; set; } = string.Empty;

        public string RealtorPhone { get; set; } = string.Empty;

        public string AgencyName { get; set; } = string.Empty; 

        public string AgencyLogoUrl { get; set; } = string.Empty;

        public int CountyId { get; set; }

        public string CountyName { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;


    }
}

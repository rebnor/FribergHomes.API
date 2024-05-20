using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FribergHomes.API.DTOs
{
    /* SalesObjectDTO that will be used in client-API requests and responses.
     * Author: Tobias 2024-04-23
     * @ Update: Switched from County-object to CountyName string / Reb 2024-05-25
     * @ Update: Corrected attribute on int/double types. / Tobias 2024-04-28
     */

    public class SalesObjectDTO
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string CreatorName { get; set; } = string.Empty;

        public DateTime? ChangeDate { get; set; }

        public string? ChangeName { get; set; } = string.Empty;

        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s-\d]+$", ErrorMessage = "Adress får enbart innehålla a-ö, 0-9, bindestreck och mellanslag")]
        [MaxLength(50, ErrorMessage = "Adress får ej överstiga 50 tecken")]
        public string Adress { get; set; } = string.Empty;

        [DisplayName("Rum")]
        [Range(1, 50, ErrorMessage = "Antal rum måste vara minst 1 och högst 50")]
        public int Rooms { get; set; }

        [DisplayName("Boarea")]
        [Range(10, 1000, ErrorMessage = "Bostadsarea måste vara minst 10 och högst 1000m2")]
        public double LivingArea { get; set; }

        [DisplayName("Biarea")]
        [Range(0, 1000, ErrorMessage = "Biarea får ej överstiga 1000m2")]
        public double? AncillaryArea { get; set; }

        [DisplayName("Tomtarea")]
        [Range(0, 1000000, ErrorMessage = "Tomtarea får ej överstiga 1000000m2")]
        public double? PlotArea { get; set; }

        [DisplayName("Driftkostnad")]
        [Range(0, 1000000, ErrorMessage = "Driftskostnad får ej överstiga 1,000,000SEK")]
        public double? YearlyCost { get; set; }

        [DisplayName("Månadsavgift")]
        [Range(0, 100000, ErrorMessage = "Månadsavgift får ej överstiga 100,000SEK")]
        public double? MonthlyFee { get; set; }

        [DisplayName("Våning")]
        [Range(0, 100, ErrorMessage = "Våning får ej överstiga 100")]
        public int? Level { get; set; }

        [DisplayName("Hiss")]
        public bool? Lift { get; set; }

        public int ListingPrice { get; set; }

        [DisplayName("Pris")]
        [Range(0, 100000000, ErrorMessage = "Pris får ej överstiga 100,0000,000SEK")]
        public int CurrentPrice { get; set; }

        [DisplayName("Objektbeskrivning")]
        public string ObjectDescription { get; set; } = string.Empty;

        [DisplayName("Byggnadsår")]
        [Range(1800, 2100, ErrorMessage = "Byggnadsår får ej vara tidigare än 1800")]
        public int BuildYear { get; set; }

        public List<string> ImageLinks { get; set; } = [];

        [DisplayName("Visningsdatum")]
        public List<DateTime> ViewingDates { get; set; } = [];

        public int RealtorId { get; set; }

        [DisplayName("Mäklare")]
        public string RealtorName { get; set; } = string.Empty;

        public string RealtorEmail { get; set; } = string.Empty;

        public string RealtorPhone { get; set; } = string.Empty;

        public int AgencyId { get; set; }

        public string AgencyName { get; set; } = string.Empty;

        public string AgencyLogoUrl { get; set; } = string.Empty;

        public int CountyId { get; set; }

        public string CountyName { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string CategoryLogoUrl { get; set; } = string.Empty;

    }
}

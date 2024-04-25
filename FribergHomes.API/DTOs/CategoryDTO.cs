using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        
        [Length(3, 30, ErrorMessage = "Kategorinamnet måste bestå av minst 3 och max 30 bokstäver")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s-]+$", ErrorMessage = "Kategorinamnet får enbart innehålla a-ö, bindestreck och mellanslag")]
        public string Name { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
    }
}

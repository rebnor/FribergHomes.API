using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
     /* Class for County (kommun).
      * Author: Tobias 2024-04-15
      */

    public class County
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kommun")]
        [Length(3, 20, ErrorMessage = "Kommunnamnet måste bestå av minst 3 och max 20 bokstäver")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s-]+$", ErrorMessage = "Kommunnamnet får enbart innehålla a-ö, bindestreck och mellanslag")]
        public required string Name { get; set; }

    }
}

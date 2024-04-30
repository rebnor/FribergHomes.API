using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.DTOs
{
    //DTO for Agency
    //Contains no list of Realtors   

    // Author: Sanna 2024-04-23
    // Update: Added attributes / Tobias 2024-04-29
    public class AgencyDTO
    {
        public int Id { get; set; }

        [Required, Display(Name = "Mäklarbyrå")]
        public string Name { get; set; } = string.Empty;

        [Required, Display(Name = "Beskrivning")]
        public string Presentation { get; set; } = string.Empty;

        [Required]
        public string Logo { get; set; } = string.Empty;

    }
}

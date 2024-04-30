using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
    // Author: Sanna 
    public class Agency
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Mäklarbyrå")]
        public string Name { get; set; }
        [Required, Display(Name = "Beskrivning")]
        public string Presentation { get; set; }
        [Required]
        public string Logo { get; set; }
        //[Display(Name = "Mäklare")]
        //public List<Realtor>? Realtors { get; set; } / Tobias 2024-04-29
    }
}

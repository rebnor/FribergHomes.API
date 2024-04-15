using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
    // Author: Sanna 
    public class Agency
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Presentation { get; set; }
        [Required]
        public string Logo { get; set; }
       
        public List<Realtor>? Realtors { get; set; }
    }
}

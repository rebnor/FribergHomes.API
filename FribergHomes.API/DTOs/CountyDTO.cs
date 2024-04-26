using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.DTOs
{
    public class CountyDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }

    }
}

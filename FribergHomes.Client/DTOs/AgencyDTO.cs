using System.ComponentModel.DataAnnotations;

namespace FribergHomes.Client.DTOs
{
    //DTO for Agency
    //Contains no list of Realtors   

    // Author: Sanna 2024-04-23
    public class AgencyDTO
    {
        public int Id { get; set; }        
        public string Name { get; set; }       
        public string Presentation { get; set; }      
        public string Logo { get; set; }       
      
    }
}

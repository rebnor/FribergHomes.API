using System.ComponentModel.DataAnnotations;

namespace FribergHomes.Client.DTOs
{
    /* DTO for Realtor
     * Instead of first&last name its fullname
     * Instead of the Agency-Object its just its name and logo
     * The list of salesobject becomes a list och salesobjectDTO
     * @ Author: Rebecka 2024-04-23        
     */
    public class RealtorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Picture { get; set; }
        public string Agency { get; set; }
        public string AgencyLogo { get; set; }
        public List<SalesObjectDTO> SalesObjects { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FribergHomes.Client.DTOs
{
    /* DTO for Realtor
     * Instead of first&last name its fullname
     * Instead of the Agency-Object its just its name and logo
     * The list of salesobject becomes a list och salesobjectDTO
     * @ Author: Rebecka 2024-04-23
     * @ Update: Commented out List<SalesObject> for testing purposes.
     *           Added property AgencyId for simplifying mapping DTO->Model / Tobias 2024-04-29
     */
    public class RealtorDTO
    {
        public int Id { get; set; }

        [Required, Display(Name = "Namn")]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required, Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, Display(Name = "Profilbild")]
        public string Picture { get; set; } = string.Empty;

        [Required]
        public int AgencyId { get; set; }

        [Display(Name = "Mäklarbyrå")]
        public string AgencyName { get; set; } = string.Empty;

        [Display(Name = "Mäklarbyrå-logo")]
        public string AgencyLogo { get; set; } = string.Empty;

        //public List<SalesObjectDTO> SalesObjects { get; set; }
    }
}

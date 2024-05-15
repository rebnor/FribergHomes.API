using FribergHomes.Client.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    /* Interface for RealtorDtos
     * @ Author: Rebecka 2024-04-24
     */
    public interface IRealtor
    {
        Task<RealtorDTO> GetRealtorByIdAsync(string id);
        Task<List<RealtorDTO>> GetAllRealtorsAsync();
        Task<List<RealtorDTO>> GetRealtorsByAgencyAsync(string agencyName);
        //Task<List<RealtorDTO>> GetRealtorsByAgencyAsync(AgencyDTO agency);
        Task<RealtorDTO> AddRealtorAsync(RealtorDTO realtor);
        Task<RealtorDTO> UpdateRealtorAsync(RealtorDTO realtor);
        Task DeleteRealtorAsync(string id);
       

    }
}

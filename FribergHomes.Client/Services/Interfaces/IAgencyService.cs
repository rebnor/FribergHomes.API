using FribergHomes.Client.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    // Author: Sanna 2024-04-24
    // Update: Added GetRealtorsAtAgency(int id) / Reb 2024-05-02
    public interface IAgencyService
    {
        Task<List<AgencyDTO>> GetAllAgenciesAsync();
        Task<AgencyDTO> GetAgencyByIdAsync(int id);
        Task<AgencyDTO> AddAgencyAsync(AgencyDTO agencyDto);

        Task<AgencyDTO> UpdateAgencyAsync(int id, AgencyDTO agencyDto);

        Task DeleteAgencyAsync(int id);

        Task<List<RealtorDTO>> GetRealtorsAtAgency(int id);


        Task<AgencyDTO> GetAgencyByRealtorEmailAsync(string email);
        Task<List<SalesObjectDTO>> GetSaleObjectsAtAgencyAsync(int id);
    }
}

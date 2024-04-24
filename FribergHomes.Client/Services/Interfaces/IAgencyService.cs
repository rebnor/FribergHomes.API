using FribergHomes.Client.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    // Author: Sanna 2024-04-24
    public interface IAgencyService
    {
        Task<List<AgencyDTO>> GetAllAgencies();
        Task<AgencyDTO> GetAgencyById(int id);
        Task AddAgencyAsync(AgencyDTO agencyDto);

        Task<AgencyDTO> UpdateAgencyAsync(AgencyDTO agencyDto);

        Task DeleteAgencyAsync(int id);

    }
}

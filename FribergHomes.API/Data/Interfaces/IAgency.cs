using FribergHomes.API.Models;

namespace FribergHomes.API.Data.Interfaces
{
    /* Class for SalesObject which forms the base class for all real estate objects.
      * Author: Tobias 2024-04-15
      * Update: Added GetRealtorsAtAgencyAsync(int id) / Reb 2024-05-02
      */

    public interface IAgency
    {
        // Possible null return
        Task<Agency?> GetAgencyByIdAsync(int id);

        Task<List<Agency>> GetAllAgenciesAsync();

        Task AddAgencyAsync(Agency agency);

        Task<Agency> UpdateAgencyAsync(Agency agency);

        Task DeleteAgencyAsync(int id);

        Task<List<Realtor>> GetRealtorsAtAgencyAsync(int id);


    }
}

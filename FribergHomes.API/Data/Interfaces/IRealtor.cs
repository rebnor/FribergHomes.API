using FribergHomes.API.Models;

namespace FribergHomes.API.Data.Interfaces
{

    // Author: Sanna 
    public interface IRealtor
    {
        Task<Realtor> GetRealtorByIdAsync(string id);
        Task<List<Realtor>> GetAllRealtorsAsync();
        Task<List<Realtor>> GetRealtorsByAgencyAsync(Agency agency);
        Task<Realtor> AddRealtorAsync(Realtor realtor);
        Task<Realtor> UpdateRealtorAsync(Realtor realtor);
        Task DeleteRealtorAsync(Realtor realtor);

        // Update: Added this because its needed in the ModelMapper / Reb 2024-04-24
        Task<Agency> GetAgencyByNameAsync(string name);
        Task<List<SalesObject>> GetRealtorsSalesObjects(Realtor realtor);


    }
}

using FribergHomes.API.Models;

namespace FribergHomes.API.Data.Interfaces
{

    // Author: Sanna 
    public interface IRealtor
    {
        Task<Realtor> GetRealtorByIdAsync(int id);
        Task<List<Realtor>> GetAllRealtorsAsync();
        Task<List<Realtor>> GetRealtorsByAgencyAsync(Agency agency);
        Task<Realtor> AddRealtorAsync(Realtor realtor);
        Task<Realtor> UpdateRealtorAsync(Realtor realtor);
        Task DeleteRealtorAsync(int id);

    }
}

using FribergHomes.API.Models;

namespace FribergHomes.API.Data.Interfaces
{
    /* Interface for County Reopsitory
    * @ Authur: Rebecka 2024-04-15
    */
    public interface ICounty
    {
        Task<County> GetCountyByIdAsync(int id);
        Task<County> GetCountyByNameAsync(string name);
        Task<List<County>> GetAllCountiesAsync(); // Eller List?
        Task<County> AddCountyAsync(County county);
        Task<County> UpdateCountyAsync(County county);
        Task DeleteCountyAsync(County county);
    }
}

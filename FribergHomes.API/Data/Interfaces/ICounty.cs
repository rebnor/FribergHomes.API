using FribergHomes.API.Models;

namespace FribergHomes.API.Data.Interfaces
{
    public interface ICounty
    {
        Task<County> GetCountyByIdAsync(int id);
        Task<County> GetCountyByNameAsync(string name);
        Task<IEnumerable<County>> GetAllCountiesAsync(); // Eller List?
        Task<County> AddCountyAsync(County county);
        Task<County> UpdateCountyAsync(County county);
        Task DeleteCountyAsync(int id);
    }
}

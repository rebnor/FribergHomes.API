using FribergHomes.Client.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    public interface ICounty
    {
        Task<County> GetCountyByIdAsync(int id);
        Task<County> GetCountyByNameAsync(string name);
        Task<List<County>> GetAllCountiesAsync(); 
        Task<County> AddCountyAsync(County county);
        Task<County> UpdateCountyAsync(County county);
        Task DeleteCountyAsync(County county);
    }
}

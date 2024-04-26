using FribergHomes.Client.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    public interface ICounty
    {
        Task<CountyDTO> GetCountyByIdAsync(int id);
        Task<CountyDTO> GetCountyByNameAsync(string name);
        Task<List<CountyDTO>> GetAllCountiesAsync(); 
        Task<CountyDTO> AddCountyAsync(CountyDTO county);
        Task<CountyDTO> UpdateCountyAsync(CountyDTO county);
        Task DeleteCountyAsync(CountyDTO county);
    }
}

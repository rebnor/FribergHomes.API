using FribergHomes.Client.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    /* Interface for CountyDTO
     * @ Author: Rebecka 2024-04-25
     */
    public interface ICounty
    {
        Task<CountyDTO> GetCountyByIdAsync(int id);
        Task<CountyDTO> GetCountyByNameAsync(string name);
        Task<List<CountyDTO>> GetAllCountiesAsync(); 
        Task<CountyDTO> AddCountyAsync(CountyDTO county);
        Task<CountyDTO> UpdateCountyAsync(CountyDTO county);
        //Task DeleteCountyAsync(CountyDTO county);
        Task DeleteCountyAsync(int id);
    }
}

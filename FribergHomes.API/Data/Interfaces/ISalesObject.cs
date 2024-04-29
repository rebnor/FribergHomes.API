using FribergHomes.API.Models;

namespace FribergHomes.API.Data.Interfaces
{
    /* Interface for SalesObject Reopsitory
     * @ Authur: Rebecka 2024-04-15
     * @ Update: Changed GetSalesObjectsByCountyAsync parameter to int id (County county). //Tobias 2024-04-23 
     * @ Update: Added a GetCountyByNameAsync method because its needed in ModelMapper / Reb 2024-05-25
     * @ Update: Added GetSalesObjectsByRealtor / Tobias 2024-04-29
     */
    public interface ISalesObject
    {
        Task<SalesObject> GetSalesObjectByIdAsync(int? id);
        Task<List<SalesObject>> GetSalesObjectsByCountyAsync(int id);
        Task<List<SalesObject>> GetSalesObjectsByRealtorAsync(int id);
        Task<List<SalesObject>> GetAllSalesObjectsAsync();
        Task<SalesObject> AddSalesObjectAsync(SalesObject salesObject);
        Task DeleteSalesObjectAsync(SalesObject salesObject);
        Task<SalesObject> UpdateSalesObjectAsync(SalesObject salesObject);

        // Update: Added this because its needed in the ModelMapper / Reb 2024-04-25
        Task<County> GetCountyByNameAsync(string countyName);
        Task<Category> GetCategoryByNameAsync(string categoryName);

    }
}

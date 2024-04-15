using FribergHomes.API.Models;

namespace FribergHomes.API.Data.Interfaces
{
    /* Interface for SalesObject Reopsitory
     * @ Authur: Rebecka 2024-04-15
     */
    public interface ISalesObject
    {
        Task<SalesObject> GetSalesObjectByIdAsync(int? id);
        Task<List<SalesObject>> GetSalesObjectsByCountyAsync(County county);
        Task<List<SalesObject>> GetAllSalesObjectsAsync();
        Task<SalesObject> AddSalesObjectAsync(SalesObject salesObject);
        Task DeleteSalesObjectAsync(SalesObject salesObject);
        Task<SalesObject> UpdateSalesObjectAsync(SalesObject salesObject);

    }
}

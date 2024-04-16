using FribergHomes.API.Models;

namespace FribergHomes.API.Data.Interfaces
{
    /* Interface for Category Reopsitory
    * @ Authur: Rebecka 2024-04-16
    */
    public interface ICategory
    {
        Task<Category> GetCategoryByIdAsync(int? id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> AddGategoryAsync(Category category);
        Task<Category> UpdateGategoryAsync(Category category);
        Task DeleteGategoryAsync(Category category);

    }
}

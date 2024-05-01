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
        //Task<Category> AddCategoryAsync(Category category);
        Task AddCategoryAsync(Category category);

        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
    }
}

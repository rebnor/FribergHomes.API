using FribergHomes.Client.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    public interface ICategory
    {
        Task<CategoryDTO> GetCategoryByIdAsync(int? id);
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> AddCategoryAsync(CategoryDTO category);
        Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO category);
        Task DeleteCategoryAsync(CategoryDTO category);

    }
}

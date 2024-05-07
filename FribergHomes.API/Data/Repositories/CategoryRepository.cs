using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomes.API.Data.Repositories
{
    /* Category Repository talking to ApplicationDBContext and inherit from Interface Category
   * All in Async.
   * @ Author: Rebecka 2024-04-16
   */
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDBContext _appDBctx;

        public CategoryRepository(ApplicationDBContext appDBctx)
        {
            _appDBctx = appDBctx;
        }

        //public async Task<Category> AddCategoryAsync(Category category)
        //{
        //    await _appDBctx.AddAsync(category);
        //    await _appDBctx.SaveChangesAsync();
        //    return category;

        //}
        public async Task AddCategoryAsync(Category category)
        {
            await _appDBctx.AddAsync(category);
            await _appDBctx.SaveChangesAsync();        
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _appDBctx.Remove(category);
            await _appDBctx.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await _appDBctx.Categories.OrderBy(c => c.Name).ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int? id)
        {
            var category = await _appDBctx.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _appDBctx.Update(category);
            await _appDBctx.SaveChangesAsync();
            return category;
        }
    }
}

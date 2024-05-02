using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace FribergHomes.Client.Services
{
    /* CategoryService that inherit from ICategory. 
     * @ Author: Rebecka 2024-04-26
     */
    public class CategoryService : ICategory
    {
        private readonly HttpClient _client;

        public CategoryService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _client.GetFromJsonAsync<List<CategoryDTO>>("api/Category");
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int? id)
        {
            return await _client.GetFromJsonAsync<CategoryDTO>($"api/Category/{id}");
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO category)
        {
            var response = await _client.PutAsJsonAsync("api/county", category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoryDTO>();
        }


        public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO category)
        {
            var response = await _client.PostAsJsonAsync("api/category", category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoryDTO>();
        }

        //public async Task DeleteCategoryAsync(CategoryDTO category)
        //{
        //    await _client.DeleteAsync($"api/category/{category.Id}");
        //}
        public async Task DeleteCategoryAsync(int id)
        {
            await _client.DeleteAsync($"api/category/{id}");
        }
    }
}

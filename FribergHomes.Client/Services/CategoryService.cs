using FribergHomes.Client.Authentications;
using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FribergHomes.Client.Services
{
    /* CategoryService that inherit from ICategory. 
     * @ Author: Rebecka 2024-04-26
     */
    public class CategoryService : ICategory
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;

        public CategoryService(HttpClient client, IAuthService authService)
        {
            _client = client;
            _authService = authService;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<List<CategoryDTO>>("api/Category");
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int? id)
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<CategoryDTO>($"api/Category/{id}");
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO category)
        {
            await SetRequestHeaders();

            var response = await _client.PutAsJsonAsync("api/county", category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoryDTO>();
        }

        public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO category)
        {
            await SetRequestHeaders();

            var response = await _client.PostAsJsonAsync("api/category", category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoryDTO>();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await SetRequestHeaders();

            await _client.DeleteAsync($"api/category/{id}");
        }

        private async Task SetRequestHeaders()
        {
            var token = await _authService.GetToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


    }
}

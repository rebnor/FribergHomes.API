using FribergHomes.Client.Authentications;
using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;

namespace FribergHomes.Client.Services
{
    /* CountyService that inherit from ICounty. 
     * @ Author: Rebecka 2024-04-25
     */
    public class CountyService : ICounty
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;

        public CountyService(HttpClient client, IAuthService authService)
        {
            _client = client;
            _authService = authService;
        }


        public async Task<List<CountyDTO>> GetAllCountiesAsync()
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<List<CountyDTO>>("/api/County");
        }

        public async Task<CountyDTO> GetCountyByIdAsync(int id)
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<CountyDTO>($"api/county/by-id/{id}");
        }

        public async Task<CountyDTO> GetCountyByNameAsync(string name)
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<CountyDTO>($"api/county/by-name/{name}");
        }



        public async Task<CountyDTO> AddCountyAsync(CountyDTO county)
        {
            await SetRequestHeaders();

            var response = await _client.PostAsJsonAsync("api/county", county);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CountyDTO>();
        }
        //public async Task DeleteCountyAsync(CountyDTO county)
        //{
        //    await _client.DeleteAsync($"api/county/{county.Id}");
        //}
        public async Task DeleteCountyAsync(int id)
        {
            await SetRequestHeaders();

            await _client.DeleteAsync($"api/county/{id}");
        }
        public async Task<CountyDTO> UpdateCountyAsync(CountyDTO county)
        {
            await SetRequestHeaders();

            var response = await _client.PutAsJsonAsync($"api/county/{county.Id}", county);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CountyDTO>();
        }

        private async Task SetRequestHeaders()
        {
            var token = await _authService.GetToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

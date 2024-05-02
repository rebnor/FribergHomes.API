using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
using System.Net;
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

        public CountyService(HttpClient client)
        {
            _client = client;
        }


        public async Task<List<CountyDTO>> GetAllCountiesAsync()
        {
            return await _client.GetFromJsonAsync<List<CountyDTO>>("/api/County");
            //var response = await _client.GetAsync("api/County");
            //response.EnsureSuccessStatusCode();
            //return await response.Content.ReadFromJsonAsync<List<CountyDTO>>();
        }

        public async Task<CountyDTO> GetCountyByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<CountyDTO>($"api/county/by-id/{id}");
        }

        public async Task<CountyDTO> GetCountyByNameAsync(string name)
        {
            return await _client.GetFromJsonAsync<CountyDTO>($"api/county/by-name/{name}");
        }



        public async Task<CountyDTO> AddCountyAsync(CountyDTO county)
        {
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
            await _client.DeleteAsync($"api/county/{id}");
        }
        public async Task<CountyDTO> UpdateCountyAsync(CountyDTO county)
        {
            var response = await _client.PutAsJsonAsync($"api/county/{county.Id}", county);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CountyDTO>();
        }
    }
}

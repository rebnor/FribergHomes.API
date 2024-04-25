using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace FribergHomes.Client.Services
{
    /* RealtorService that inherit from IRealtor. 
     * @ Author: Rebecka 2024-04-24
     */
    public class RealtorService : IRealtor
    {
        private readonly HttpClient _client;

        public RealtorService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<RealtorDTO>> GetAllRealtorsAsync()
        {
            return await _client.GetFromJsonAsync<List<RealtorDTO>>("api/Realtor");
        }
        public async Task<RealtorDTO> GetRealtorByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<RealtorDTO>($"api/realtor/{id}");
        }

        public async Task<RealtorDTO> AddRealtorAsync(RealtorDTO realtorDto)
        {
            var response = await _client.PostAsJsonAsync("api/realtor", realtorDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RealtorDTO>();
        }

        public async Task DeleteRealtorAsync(int id)
        {
            await _client.DeleteAsync($"api/realtor/{id}");
        }

        public async Task<RealtorDTO> UpdateRealtorAsync(RealtorDTO realtor)
        {
            var response = await _client.PutAsJsonAsync($"api/realtor/{realtor.Id}", realtor);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RealtorDTO>();
        }

        public async Task<List<RealtorDTO>> GetRealtorsByAgencyAsync(AgencyDTO agency)
        {
            return await _client.GetFromJsonAsync<List<RealtorDTO>>($"api/realtor/{agency.Name}");
        }
    }
}

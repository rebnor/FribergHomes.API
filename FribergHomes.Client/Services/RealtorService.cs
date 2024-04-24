using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace FribergHomes.Client.Services
{
    public class RealtorService
    {
        private readonly HttpClient _client;

        public RealtorService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<RealtorDTO>> GetAllRealtors()
        {
            return await _client.GetFromJsonAsync<List<RealtorDTO>>("api/Realtor");
        }

        public async Task<RealtorDTO> GetRealtorById(int id)
        {
            return await _client.GetFromJsonAsync<RealtorDTO>($"api/realtor/{id}");
        }

    }
}

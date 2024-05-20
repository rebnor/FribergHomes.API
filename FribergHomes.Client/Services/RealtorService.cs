using FribergHomes.Client.Authentications;
using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace FribergHomes.Client.Services
{
    /* RealtorService that inherit from IRealtor. 
     * @ Author: Rebecka 2024-04-24
     * @ Update: När en mäklare raderas måste salesobjekt gå till ny mäklare / Reb 2024-05-17
     */
    public class RealtorService : IRealtor
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;

        public RealtorService(HttpClient client, IAuthService authService)
        {
            _client = client;
            _authService = authService;
        }
        public async Task<List<RealtorDTO>> GetAllRealtorsAsync()
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<List<RealtorDTO>>("api/Realtor");
        }
        public async Task<RealtorDTO> GetRealtorByIdAsync(string id)
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<RealtorDTO>($"api/realtor/{id}");
        }

        public async Task<RealtorDTO> AddRealtorAsync(RealtorDTO realtorDto)
        {
            await SetRequestHeaders();

            var response = await _client.PostAsJsonAsync("api/realtor", realtorDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RealtorDTO>();
        }

        public async Task DeleteRealtorAsync(string realtorId, string newRealtorId)
        {
            await SetRequestHeaders();

            await _client.DeleteAsync($"api/realtor/{realtorId}/{newRealtorId}");
        }

        public async Task<RealtorDTO> UpdateRealtorAsync(RealtorDTO realtorDto)
        {
            await SetRequestHeaders();

            var response = await _client.PutAsJsonAsync($"api/realtor/{realtorDto.Id}", realtorDto);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Ett fel uppstod vid PUT-anropet, felmeddelande: {response.StatusCode}");
            }
            var updatedrealtorDTO = await response.Content.ReadFromJsonAsync<RealtorDTO>();
            return updatedrealtorDTO;
           

        }

        //public async Task<List<RealtorDTO>> GetRealtorsByAgencyAsync(AgencyDTO agency)
        //{
        //    return await _client.GetFromJsonAsync<List<RealtorDTO>>($"api/realtor/{agency.Name}");
        //}

        public async Task<List<RealtorDTO>> GetRealtorsByAgencyAsync(string agencyName)
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<List<RealtorDTO>>($"api/realtor/by-agency/{agencyName}");
        }

        private async Task SetRequestHeaders()
        {
            var token = await _authService.GetToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}

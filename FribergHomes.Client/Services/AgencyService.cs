using FribergHomes.Client.Authentications;
using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;

namespace FribergHomes.Client.Services
{
    // Author: Sanna 2024-04-25
    // Update: Added GetRealtorsAtAgency(int id) Reb 2024-05-02
    public class AgencyService : IAgencyService
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;

        public AgencyService(HttpClient client, IAuthService authService)
        {
            _client = client;
            _authService = authService;
        }

        public async Task<List<AgencyDTO>> GetAllAgenciesAsync()
        {
            await SetRequestHeaders();

            try
            {
                var response = await _client.GetAsync("/api/Agency");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ett fel uppstod vid GET-anropet, felmeddelande: {response.StatusCode}");
                }
                var agenciesDto = await response.Content.ReadFromJsonAsync<List<AgencyDTO>>();
                return agenciesDto;
            }
            catch (Exception)
            {
                throw new Exception("Ett oväntat fel uppstod.");
            }

        }

        public async Task<AgencyDTO> GetAgencyByIdAsync(int id)
        {
            await SetRequestHeaders();

            try
            {
                var response = await _client.GetAsync($"/api/Agency/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ett fel uppstod vid GET-anropet, felmeddelande: {response.StatusCode}");
                }
                var agencyDto = await response.Content.ReadFromJsonAsync<AgencyDTO>();
                return agencyDto;
            }
            catch (Exception)
            {
                throw new Exception("Ett oväntat fel uppstod.");
            }
        }

        public async Task<List<RealtorDTO>> GetRealtorsAtAgency(int id)
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<List<RealtorDTO>>($"api/agency/realtors/{id}");
        }

        public async Task<AgencyDTO> AddAgencyAsync(AgencyDTO agencyDto)
        {
            await SetRequestHeaders();

            try
            {
                var response = await _client.PostAsJsonAsync($"/api/Agency/", agencyDto);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ett fel uppstod vid POST-anropet, felmeddelande: {response.StatusCode}");
                }
                var createdAgency = await response.Content.ReadFromJsonAsync<AgencyDTO>();
                return createdAgency;
            }
            catch (Exception)
            {
                throw new Exception("Ett oväntat fel uppstod.");
            }
        }

        public async Task<AgencyDTO> UpdateAgencyAsync(int id, AgencyDTO agencyDto)
        {
            await SetRequestHeaders();

            try
            {
                var response = await _client.PutAsJsonAsync($"/api/Agency/{id}", agencyDto);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ett fel uppstod vid PUT-anropet, felmeddelande: {response.StatusCode}");
                }
                var updatedAgency = await response.Content.ReadFromJsonAsync<AgencyDTO>();
                return updatedAgency;
            }
            catch (Exception)
            {
                throw new Exception("Ett oväntat fel uppstod.");
            }
        }

        public async Task DeleteAgencyAsync(int id)
        {
            await SetRequestHeaders();

            try
            {
                var response = await _client.DeleteAsync($"/api/Agency/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ett fel uppstod vid DELETE-anropet, felmeddelande: {response.StatusCode}");
                }
            }
            catch (Exception)
            {
                throw new Exception("Ett oväntat fel uppstod.");
            }
        }

        public async Task<AgencyDTO> GetAgencyByRealtorEmailAsync(string email) 
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<AgencyDTO>($"api/agency/by-email/{email}");
        }

        public async Task<List<SalesObjectDTO>> GetSaleObjectsAtAgencyAsync(int id)
        {
            await SetRequestHeaders();

            return await _client.GetFromJsonAsync<List<SalesObjectDTO>>($"api/agency/saleobjects/{id}");
        }

        private async Task SetRequestHeaders()
        {
            var token = await _authService.GetToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

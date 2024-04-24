using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace FribergHomes.Client.Services
{
    public class AgencyService : IAgencyService
    {
        private readonly HttpClient _client;

        public AgencyService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<AgencyDTO>> GetAllAgencies()
        {

            return await _client.GetFromJsonAsync<List<AgencyDTO>>($"/api/Agency");
        }

        public async Task<AgencyDTO> GetAgencyById(int id)
        {
            return await _client.GetFromJsonAsync<AgencyDTO>($"/api/Agency/{id}");
        }

        public async Task AddAgencyAsync(AgencyDTO agencyDto)
        {
            throw new NotImplementedException();
        }

        public Task<AgencyDTO> UpdateAgencyAsync(AgencyDTO agencyDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAgencyAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using FribergHomes.Client.DTOs;

namespace FribergHomes.Client.Services.Interfaces
{
    public class CountyService : ICounty
    {
        private readonly HttpClient _client;

        public CountyService(HttpClient client)
        {
            _client = client;
        }

        public Task<List<County>> GetAllCountiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<County> GetCountyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<County> GetCountyByNameAsync(string name)
        {
            throw new NotImplementedException();
        }






        public Task<County> AddCountyAsync(County county)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCountyAsync(County county)
        {
            throw new NotImplementedException();
        }

        public Task<County> UpdateCountyAsync(County county)
        {
            throw new NotImplementedException();
        }
    }
}

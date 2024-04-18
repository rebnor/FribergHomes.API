using FribergHomes.API.Data;
using FribergHomes.API.Models;
using System.Text.Json;

namespace FribergHomes.API.Seeders
{
    /* Seeder class that checks if DB table County contains any entries.
     * If not, retrieves "kommun" data from Sveriges Kommuner och Regioner (https://catalog.skl.se/)
     * and populates the DB table.
     * Author: Tobias 2024-04-17
     */

    public class CountySeeder
    {
        private readonly ApplicationDBContext _appDbContext;
        private readonly IConfiguration _config;

        public CountySeeder(ApplicationDBContext dBContext, IConfiguration configuration)
        {
            _appDbContext = dBContext;
            _config = configuration;
        }


        private async Task SeedCounties()
        {
            if (!_appDbContext.Counties.Any())
            {
                var apiKey = _config["AppSettings:ExternalApi:SKR"];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new("https://catalog.skl.se/rowstore/");
                    HttpResponseMessage response = await client.GetAsync($"/dataset/{apiKey}?_limit=300&_offset=0");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        List<CountyData> counties = [JsonSerializer.Deserialize<CountyData>(content)];

                    }
                }

            }
        }

        // Helperclass to store API call results.
        private class CountyData
        {
            public int Inhabitants { get; set; }
            public string MainGroup { get; set; } = string.Empty;
            public string Municipality { get; set; } = string.Empty;
            public string MunicipalityGroup2023 { get; set; } = string.Empty;
            public string MunicipalityCode { get; set; } = string.Empty;
            public string MunicipalityGroup2017 { get; set; } = string.Empty;
            public string GroupAssignation { get; set; } = string.Empty;
            public int InhabitantsClosestCity { get; set; }
        }
    }

    
}

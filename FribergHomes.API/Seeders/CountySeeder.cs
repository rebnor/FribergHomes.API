using FribergHomes.API.Data;
using FribergHomes.API.Models;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace FribergHomes.API.Seeders
{
    /* Seeder class that checks if DB table County contains any entries.
     * If not, retrieves "kommun" data from Sveriges Kommuner och Regioner (https://catalog.skl.se/)
     * and populates the DB table.
     * Author: Tobias 2024-04-18
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

        public async Task SeedCounties()
        {
            if (!_appDbContext.Counties.Any())
            {
                var apiKey = _config["ExternalApi:SKR"];
                List<County> counties = new();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new("https://catalog.skl.se/rowstore/");
                    HttpResponseMessage response = await client.GetAsync($"dataset/{apiKey}?_limit=300&_offset=0");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        JObject jObject = JObject.Parse(content);
                        var countyDataArray = jObject["results"];

                        foreach (var countyJson in countyDataArray)
                        {
                            var countyName = countyJson["kommun"]?.ToString();

                            if ( countyName != null)
                            {
                                counties.Add(new County { Name = countyName });
                            }
                        }

                        counties.Sort((county1, county2) => 
                            string.Compare(county1.Name, county2.Name, StringComparison.OrdinalIgnoreCase));

                        try
                        {
                            await _appDbContext.Counties.AddRangeAsync(counties);
                            _appDbContext.SaveChanges();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Något gick fel vid lagring av kommunobjekt till databasen!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Något gick fel vid datahämtningen!");
                    }
                }
            }
        }

    }
}

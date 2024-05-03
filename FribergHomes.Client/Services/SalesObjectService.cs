using System.Net.Http.Json;
using System.Net;
using System.Runtime.Serialization;
using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;

namespace FribergHomes.Client.Services
{

    /* Service that manages API requests and responses related to SalesObjectDTOs.
     * Author: Tobias 2024-04-24
     * 
     * Update: Added GetAllByRealtor method. Changed name of GetAll(countId) to GetAllByCounty / Tobias 2024-04-29
     */

    public class SalesObjectService : ISalesObject
    {

        private readonly HttpClient _client;

        public SalesObjectService(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// GET request for a specific SalesObject in the database.
        /// Throws exception if HTTP status code is not in the 200-range or if object cannot be found/return is null.
        /// </summary>
        /// <param name="id">SalesObjectDTO Id</param>
        /// <returns>A SalesObjectDTO object.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<SalesObjectDTO> Get(int id)
        {
            var response = await _client.GetAsync($"api/salesobject/{id}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new HttpRequestException($"Försäljningsobjekt med id: {id} existerar ej!");
                }

                throw new HttpRequestException($"Något gick fel vid datahämtningen!");
            }

            var salesObject = await response.Content.ReadFromJsonAsync<SalesObjectDTO>();

            if (salesObject == null)
            {
                throw new SerializationException("Något gick fel vid datainläsning!");
            }

            return salesObject;
        }


        /***Test Rebecka**/
        public async Task<List<SalesObjectDTO>> GetSalesByCounty(string name)
        {
            var response = await _client.GetAsync($"api/salesobject/county-name/{name}");
            var salesObjects = await response.Content.ReadFromJsonAsync<List<SalesObjectDTO>>();
            if (salesObjects == null)
            {
                return null;
            }

            return salesObjects;
        }


        /// <summary>
        /// GET method that retrieves all SalesObjects from the database.
        /// Throws exception if HTTP status code is not in the 200-range.
        /// </summary>
        /// <returns>A List&lt;SalesObjectDTO&gt;</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<SalesObjectDTO>> GetAll()
        {
            var response = await _client.GetAsync("api/salesobject");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Något gick fel vid hämtningen av data!");
            }

            var salesObjects = await response.Content.ReadFromJsonAsync<List<SalesObjectDTO>>();

            return salesObjects ?? new List<SalesObjectDTO>();
        }


        /// <summary>
        /// GET method that retrieves all SalesObjects with specific county id from the database.
        /// Throws exception if HTTP status code is not in the 200-range.
        /// </summary>
        /// <param name="int id"></param>
        /// <returns>A List&lt;SalesObjectDTO&gt;</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<SalesObjectDTO>> GetAllByCounty(int id)
        {
            var response = await _client.GetAsync($"api/salesobject/county/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Något gick fel vid hämtningen av data!");
            }

            var salesObjects = await response.Content.ReadFromJsonAsync<List<SalesObjectDTO>>();

            return salesObjects ?? new List<SalesObjectDTO>();
        }

        /// <summary>
        /// GET method that retrieves all SalesObjects with specific realtor id from the database.
        /// Throws exception if HTTP status code is not in the 200-range.
        /// </summary>
        /// <param name="int id"></param>
        /// <returns>A List&lt;SalesObjectDTO&gt;</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<SalesObjectDTO>> GetAllByRealtor(int id)
        {
            var response = await _client.GetAsync($"api/salesobject/realtor/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Något gick fel vid hämtningen av data!");
            }

            var salesObjects = await response.Content.ReadFromJsonAsync<List<SalesObjectDTO>>();

            return salesObjects ?? new List<SalesObjectDTO>();
        }

        /// <summary>
        /// PUT method that updates a specific SalesObject in the database and, if successful, returns the updated object.
        /// Throws exception if HTTP status code is not in the 200-range.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salesObject"></param>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<SalesObjectDTO> Update(int id, SalesObjectDTO salesObject)
        {
            var response = await _client.PutAsJsonAsync($"/api/salesobject/{id}", salesObject);

            response.EnsureSuccessStatusCode();

            var updatedSalesObject = await response.Content.ReadFromJsonAsync<SalesObjectDTO>();

            return updatedSalesObject!;
        }

        /// <summary>
        /// POST method that creates a new SalesObject in the database.
        /// Throws exception if HTTP status code is not in the 200-range or problem occurs during deserialization of returned object.
        /// </summary>
        /// <param name="salesObject"></param>
        /// <returns>The saved SalesObjectDTO object.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="SerializationException"></exception>
        public async Task<SalesObjectDTO> Create(SalesObjectDTO salesObject)
        {
            var response = await _client.PostAsJsonAsync<SalesObjectDTO>("/api/salesobject", salesObject);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Något gick fel vid lagring av det nya försäljningsobjektet");
            }

            var createdSalesObject = await response.Content.ReadFromJsonAsync<SalesObjectDTO>();

            if (createdSalesObject == null)
            {
                throw new SerializationException("Något gick fel vid datainläsning!");
            }

            return createdSalesObject;
        }

        /// <summary>
        /// DELETE method that deletes a SalesObject in the database.
        /// Throws exception if HTTP status code is not in the 200-range.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="HttpRequestException"></exception>
        public async Task Delete(int id)
        {
            var response = await _client.DeleteAsync($"/api/salesobject/{id}");

            response.EnsureSuccessStatusCode();
        }

    }
}

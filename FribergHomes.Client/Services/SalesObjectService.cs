﻿using System.Net.Http.Json;
using System.Net;
using System.Runtime.Serialization;
using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;

namespace FribergHomes.Client.Services
{
    public class SalesObjectService : ISalesObject
    {
        /* Repository that manages API requests and responses related to SalesObjects.
         * Author: Tobias 2024-04-24
         */

        private readonly HttpClient _client;

        public SalesObjectService(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// GET request for a specific SalesObject in the database.
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

        /// <summary>
        /// GET method that retrieves all SalesObjects from the database.
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
        /// </summary>
        /// <param name="countyId"></param>
        /// <returns>A List&lt;SalesObjectDTO&gt;</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<SalesObjectDTO>> GetAll(int countyId)
        {
            var response = await _client.GetAsync($"api/salesobject/county/{countyId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Något gick fel vid hämtningen av data!");
            }

            var salesObjects = await response.Content.ReadFromJsonAsync<List<SalesObjectDTO>>();

            return salesObjects ?? new List<SalesObjectDTO>();
        }

        /// <summary>
        /// PUT method that updates a specific SalesObject in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salesObject"></param>
        /// <exception cref="HttpRequestException"></exception>
        public async Task Update(int id, SalesObjectDTO salesObject)
        {
            var response = await _client.PutAsJsonAsync($"/api/salesobject/{id}", salesObject);

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// POST method that creates a new SalesObject in the database.
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

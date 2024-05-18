﻿using FribergHomes.Client.DTOs;
using FribergHomes.Client.Services.Interfaces;
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

        public RealtorService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<RealtorDTO>> GetAllRealtorsAsync()
        {
            return await _client.GetFromJsonAsync<List<RealtorDTO>>("api/Realtor");
        }
        public async Task<RealtorDTO> GetRealtorByIdAsync(string id)
        {
            return await _client.GetFromJsonAsync<RealtorDTO>($"api/realtor/{id}");
        }

        public async Task<RealtorDTO> AddRealtorAsync(RealtorDTO realtorDto)
        {
            var response = await _client.PostAsJsonAsync("api/realtor", realtorDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RealtorDTO>();
        }

        public async Task DeleteRealtorAsync(string realtorId, string newRealtorId)
        {
            await _client.DeleteAsync($"api/realtor/{realtorId}/{newRealtorId}");
        }

        public async Task<RealtorDTO> UpdateRealtorAsync(string id, RealtorDTO realtorDto)
        {

            var response = await _client.PutAsJsonAsync($"api/realtor/{id}", realtorDto);
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
            return await _client.GetFromJsonAsync<List<RealtorDTO>>($"api/realtor/by-agency/{agencyName}");
        }

    }
}

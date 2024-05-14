using FribergHomes.Client.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
/* @ Author: Reb 2024-05-13
 * @ Update: GetToken skickar hela AuthResponse ist för endast token-sträng / Reb 2024-05-14*/
namespace FribergHomes.Client.Helper
{
    public class TokenHandler
    {
        private readonly HttpClient _httpClient;

        public TokenHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<string> GetToken(string email, string password)
        public async Task<AuthResponseDTO> GetToken(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new
            {
                Email = email,
                Password = password
            });

            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

                //var userId = authResponse.UserId;
                //var token = authResponse.Token;

                return authResponse;
            }

            throw new Exception("Fel användarnamn och/eller lösenord. Försök igen!");
        }
        public async Task<string> RegisterAndGenerateToken(RegisterRealtorDTO realtorData)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", realtorData);
                response.EnsureSuccessStatusCode();

                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
                return authResponse.Token;
            }
            catch (Exception ex)
            {
                throw new Exception("Registrering av användare misslyckades.");
            }
        }

        //public void AddTokenToRequest(HttpRequestMessage request, string token)
        //{
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}
        public void ConfigureHttpClientWithToken(HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}

using Blazored.LocalStorage;
using FribergHomes.Client.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;



/* @ Author: Reb 2024-05-13
 * @ Update: GetToken skickar hela AuthResponse ist för endast token-sträng / Reb 2024-05-14*/
namespace FribergHomes.Client.Authentications
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        //public async Task<string> GetToken(string email, string password)
        public async Task<AuthenticationState> LogIn(string email, string password)
        //public async Task<Task<AuthenticationState>> LogIn(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new
            {
                Email = email,
                Password = password
            });

            if (response.IsSuccessStatusCode)
            {

                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

                await _localStorage.SetItemAsync("jwt", authResponse.Token); // lagrar token i localStorage som jwt

                ConfigureHttpClientWithToken(_httpClient, authResponse.Token); // Lägger token/jwt i Bearer/Header

                var authState = await _authStateProvider.GetAuthenticationStateAsync(); // hämtar authentication state

                return authState; // skickar authentication state


                //var isSucess = authState.IsCompletedSuccessfully;
                //if (isSucess)
                //{
                //    return true;
                //    //ConfigureHttpClientWithToken(_httpClient, jwt); // Lägger token/jwt i Bearer/Header

                //    //return authResponse;
                //}
            }

            throw new Exception("Fel användarnamn och/eller lösenord. Försök igen!");
        }
        public async Task<string> Register(RegisterRealtorDTO realtorData)
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

        public async Task LogOut()
        {

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

using Blazored.LocalStorage;
using FribergHomes.Client.Constants;
using FribergHomes.Client.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

/* @ Author: Reb 2024-05-13
 * @ Update: GetToken skickar hela AuthResponse ist för endast token-sträng / Reb 2024-05-14
 * @Update: Bytt namn på metoder, uppdaterat med localstorage, authstate, authprovider och arv av Iauthstate / Reb 2024-05-14+15 */
namespace FribergHomes.Client.Authentications
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthProvider _authStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage,AuthProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<AuthenticationState> LogIn(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new
            {
                Email = email,
                Password = password
            });

            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>(); // hämtar svar
                await _localStorage.SetItemAsync("jwt", authResponse.Token); // lagrar token i localStorage som jwt
                ConfigureHttpClientWithToken(_httpClient, authResponse.Token); // Lägger token/jwt i Bearer/Header
                var authState = await _authStateProvider.GetAuthenticationStateAsync(); // hämtar authentication state
                return authState; // skickar authentication state
            }

            throw new Exception("Fel användarnamn och/eller lösenord. Försök igen!");
        }
        public async Task<string> Register(RegisterRealtorDTO realtorData)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", realtorData); // registrerar och hämtar svar
                response.EnsureSuccessStatusCode(); // ser över att det gick bra
                //var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>(); // hämtar authDTO svaret // TODO: <-- Tydligen skrivs inte i Json...
                var authResponse = await response.Content.ReadAsStringAsync(); // Behövdes läggas till för att fungera.
                return authResponse; // skickar response
            }
            catch (Exception ex)
            {
                throw new Exception("Registrering av användare misslyckades." + ex.Message);
            }
        }

        public async Task LogOut()
        {
            await _authStateProvider.LogOutAsync();
            RemoveTokenFromHttpClient(_httpClient); // Tar bort token/jwt i Bearer/Header ??
        }

        public void ConfigureHttpClientWithToken(HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public void RemoveTokenFromHttpClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<AuthenticationState> CheckAuthState()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            return state;
        }


        /// Author: Tobias 2024-05-15
        /// <summary>
        /// Method to get Id of currently logged in user.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserId()
        {
            if (_authStateProvider is not null)
            {
                var authState = await _authStateProvider.GetAuthenticationStateAsync();
                var userIdClaim = authState.User.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Uid);

                if (userIdClaim is not null)
                {
                    var userId = userIdClaim.Value;

                    return userId;
                }
            }

            return string.Empty;
        }

    }
}

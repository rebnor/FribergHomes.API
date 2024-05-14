using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FribergHomes.Client.Authentications
{
    public class AuthProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public AuthProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var jwt = await _localStorage.GetItemAsync<string>("jwt"); // hämtar token som ligger i localStorage jwt

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var jwtContent = jwtSecurityTokenHandler.ReadJwtToken(jwt); // Hämtar token innehåll

            var claims = jwtContent.Claims.ToList(); // hämtar claims i token
            claims.Add(new Claim(ClaimTypes.Name, jwtContent.Subject));

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")); // hämtar användare med denna token

            var authState = Task.FromResult(new AuthenticationState(user)); // Sätter user i ny authentication state

            NotifyAuthenticationStateChanged(authState); // megafon om att state är ändrad
           
            return authState.Result;
        }
    }
}

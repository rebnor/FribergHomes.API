using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
/* Author Rebecka 2024-05-14+15
 */
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
            var user = new ClaimsPrincipal(new ClaimsIdentity()); // gör en ny tom användare
            var jwt = await _localStorage.GetItemAsync<string>("jwt"); // hämtar token som ligger i localStorage jwt
            // Om jwt är tom eller null returneras ett AuthenticationState med tomt ClaimsPrinciple.// Tobias
            if (string.IsNullOrEmpty(jwt))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtContent = jwtSecurityTokenHandler.ReadJwtToken(jwt); // Hämtar token innehåll
            if (jwtContent.ValidTo < DateTime.UtcNow) // Ser över om tiden har gått ut på token
            {
                return new AuthenticationState(user); // Skickar ny state med tomma användaren
            }
            var claims = jwtContent.Claims.ToList(); // hämtar claims i token
            claims.Add(new Claim(ClaimTypes.Name, jwtContent.Subject));
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")); // uppdaterar användare med denna token
            var authState = Task.FromResult(new AuthenticationState(user)); // Sätter user i ny authentication state
            NotifyAuthenticationStateChanged(authState); // megafon om att state är ändrad
            return authState.Result; // skickar state svar
        }

        public async Task LogOutAsync()
        {
            string? jwt = await _localStorage.GetItemAsync<string>("jwt"); // hämtar jwt
            if (string.IsNullOrEmpty(jwt))
            {
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            //await _localStorage.RemoveItemAsync("jwt"); // raderar jwt
            await _localStorage.ClearAsync();
            ClaimsPrincipal? emptyClaim = new ClaimsPrincipal(new ClaimsIdentity()); // gör en tom claim-user
            var authstate = Task.FromResult(new AuthenticationState(emptyClaim)); // sätter state till den tomma claim-user
            NotifyAuthenticationStateChanged(authstate); // megafon om att state är ändrad
        }
    }
}

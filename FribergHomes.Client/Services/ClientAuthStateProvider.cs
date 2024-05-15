using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace FribergHomes.Client.Services
{
    public class ClientAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ClaimsPrincipal _noUser;
        

        public ClientAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _noUser = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {


                var storedToken = await _localStorage.GetItemAsStringAsync("jwt");

                // If no token present in localstorage, empty claimsprincipal is passed to AuthStateProvider.

                if (string.IsNullOrEmpty(storedToken))
                {
                    return new AuthenticationState(_noUser);
                }

                // If token is present in localstorage, try to extract claims and pass to AuthStateProvider.
                var jwt = new JsonWebToken(storedToken);

                var claims = jwt.Claims;

                var identity = new ClaimsIdentity(claims, "jwt");

                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);

            }
            catch (Exception)
            {
                return new AuthenticationState(_noUser);
            }
        }


    }
}

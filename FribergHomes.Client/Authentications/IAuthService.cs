using FribergHomes.Client.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace FribergHomes.Client.Authentications
{
    public interface IAuthService
    {
        public Task<AuthenticationState> LogIn(string email, string password);
        public Task<string> Register(RegisterRealtorDTO realtorData);
        public Task LogOut();
        public Task<AuthenticationState> CheckAuthState();
        Task<string> GetUserId();
        Task<string> GetUserName();
    }
}

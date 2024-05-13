using FribergHomes.API.DTOs;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Identity;

namespace FribergHomes.API.Services
{
    public interface IAuthService
    {

        Task<AuthResponseDTO> Login(LoginRealtorDTO credentials);

        Task<IdentityResult> Register(RegisterRealtorDTO realtor);

        Task ValidateToken(string token);

        Task RenewToken(string token);

        Task<Realtor> GetRealtorFromToken(string token);
    }
}

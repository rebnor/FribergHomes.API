using FribergHomes.API.Constants;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FribergHomes.API.Services
{
    // Author: Tobias 2024-05-13

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<Realtor> _userManager;
        
        private readonly IAgency _agencyRepo;

        public AuthService(IConfiguration config, UserManager<Realtor> userManager, IAgency agencyRepo)
        {
            _config = config;
            _userManager = userManager;
            _agencyRepo = agencyRepo;
        }

        /// <summary>
        /// Realtor login method
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public async Task<AuthResponseDTO> Login(LoginRealtorDTO credentials)
        {
            var realtor = await _userManager.FindByEmailAsync(credentials.Email);
            var passwordValid = await _userManager.CheckPasswordAsync(realtor, credentials.Password);

            var response = new AuthResponseDTO();

            if (realtor != null && passwordValid)
            {
                response.Email = credentials.Email;
                response.Token = await GenerateToken(realtor);
                response.UserId = realtor!.Id;
                response.ValidCredentials = true;
            }

            return response;
        }

        public async Task<IdentityResult> Register(RegisterRealtorDTO realtorData)
        {

            //TODO: Uppdatera AutoMapper + resolver för Realtor, implementera sedan här! / Tobias
            Realtor realtor = new()
            {
                UserName = realtorData.Email,
                Email = realtorData.Email,
                FirstName = realtorData.FirstName,
                LastName = realtorData.LastName,
                PhoneNumber = realtorData.PhoneNumber,
                Agency = await _agencyRepo.GetAgencyByIdAsync(realtorData.AgencyId)
            };

            var result = await _userManager.CreateAsync(realtor, realtorData.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(realtor, ApiRoles.Realtor);
            }

            return result;

        }

        private async Task<string> GenerateToken(Realtor realtor)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(realtor);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(realtor);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, realtor.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, realtor.Email),
                new Claim(CustomClaimTypes.Uid, realtor.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

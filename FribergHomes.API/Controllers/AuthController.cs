using FribergHomes.API.Constants;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FribergHomes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Realtor> _userManager;
        private readonly IAgency _agencyRepo;
        private readonly IRealtor _realtorRepo;
        private readonly IConfiguration _config;

        public AuthController(UserManager<Realtor> userManager, IAgency agencyRepo, IRealtor realtorRepo, IConfiguration config)
        {
            _userManager = userManager;
            _agencyRepo = agencyRepo;
            _realtorRepo = realtorRepo;
            _config = config;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRealtorDTO realtorDto)
        {
            try
            {
                Realtor realtor = new()
                {
                    UserName = realtorDto.Email,
                    Email = realtorDto.Email,
                    FirstName = realtorDto.FirstName,
                    LastName = realtorDto.LastName,
                    PhoneNumber = realtorDto.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Agency = await _agencyRepo.GetAgencyByIdAsync(realtorDto.AgencyId)
                };

                var result = await _userManager.CreateAsync(realtor, realtorDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(realtor, ApiRoles.Admin);
                return Accepted();

            }
            catch (Exception ex)
            {
                return Problem($"Något gick fel i {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginRealtorDTO realtorDto)
        {
            // Testa email
            // Verifiera password
            //var realtor = _realtorRepo.GetRealtor

            try
            {
                var realtor = await _userManager.FindByEmailAsync(realtorDto.Email);
                var passwordValid = await _userManager.CheckPasswordAsync(realtor, realtorDto.Password);
                
                if (realtor == null || passwordValid == false )
                {
                    return Unauthorized(realtorDto);
                }

                string tokenString = await GenerateToken(realtor);

                var response = new AuthResponseDTO
                {
                    Email = realtorDto.Email,
                    Token = tokenString,
                    UserId = realtor.Id
                };

                return Accepted(response);
            }
            catch (Exception)
            {
                return Problem($"Något gick fel vid inloggningsförsöket", statusCode: 500);
            }
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

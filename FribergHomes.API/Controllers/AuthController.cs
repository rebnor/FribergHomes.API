using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FribergHomes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Realtor> _userManager;
        private readonly IAgency _agencyRepo;

        public AuthController(UserManager<Realtor> userManager, IAgency agencyRepo)
        {
            _userManager = userManager;
            _agencyRepo = agencyRepo;
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

                await _userManager.AddToRoleAsync(realtor, "Realtor");
                return Accepted();

            }
            catch (Exception ex)
            {
                return Problem($"Något gick fel i {nameof(Register)}", statusCode: 500);
            }
        }
    }
}

using FribergHomes.API.Constants;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;
using FribergHomes.API.Services;
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
    /* API controller to handle HTTP requests and responses related to Realtor objects.
     * Author: Tobias 2024-05-09
     */ 

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// AuthController API endpoint for creating and assigning roles to new realtors
        /// </summary>
        /// <param name="realtorData"></param>
        /// <returns>Http status code representing success/failed registration attempt.</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRealtorDTO realtorData)
        {
            try
            {
                var result = await _authService.Register(realtorData);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Accepted();

            }
            catch (Exception ex)
            {
                return Problem($"Något gick fel i {nameof(Register)}", statusCode: 500);
            }
        }

        /// <summary>
        /// AuthController API endpoint for realtor login.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>If login attempt is successful, AuthResponseDTO containing realtor data and token string.</returns>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginRealtorDTO credentials)
        {
            try
            {
                var response = await _authService.Login(credentials);
                
                if (!response.ValidCredentials)
                {
                    return Unauthorized("Felaktiga inloggningsuppgifter!");
                }

                return Accepted(response);
            }
            catch (Exception)
            {
                return Problem($"Något gick fel vid inloggningsförsöket", statusCode: 500);
            }
        }

    }
}

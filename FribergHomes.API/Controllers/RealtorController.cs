using FribergHomes.API.Data;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Mappers;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Mvc;
using FribergHomes.API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using FribergHomes.API.Constants;
using Microsoft.AspNetCore.Authorization;


namespace FribergHomes.API.Controllers
{

    /* API controller to handle HTTP requests and responses related to Realtor objects.
     * Author: Tobias 2024-04-16
     * Revised: Tobias 2024-04-18 Implemented exception handling and
     * generalFaultMessage for status code 500 responses.
     * @ Update: Switched from Realtor-object to RealtorDTO-object 
     *          Added DtoToRealtor in Post & Put methods 
     *          Added GetRealtorByAgency, it was already in Repository but wasnt used here
     *          PutRealtor - removed id parameter and comparison to RealtorDTO.Id // Reb 2024-04-24
     * @Update: När är Realtor raderas ska en ny mäklare få alla dess SalesObject // Reb 2024-05-17
     * Revised: Implemented Authorize-attributes on selected endpoints /Tobias 2024-05-20
     */

    [Route("api/[controller]")]
    [ApiController]
    public class RealtorController : ControllerBase
    {
        private readonly string _generalFaultMessage = "Ett oväntat fel uppstod vid hanteringen av förfrågan!";
        private readonly IRealtor _realtorRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Realtor> _userManager;

        public RealtorController(IRealtor realtorRepository, IMapper mapper, UserManager<Realtor> userManager)
        {
            _realtorRepository = realtorRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET method that returns a list of all Realtor objects stored in the DB.
        // GET: api/<RealtorController>
        [HttpGet]
        public async Task<ActionResult<List<RealtorDTO>>> GetRealtors()
        {
            try
            {
                var realtors = await _realtorRepository.GetAllRealtorsAsync();
                
                if(realtors.Any())
                {
                    List<RealtorDTO> realtorDtos = new();
                    foreach (var realtor in realtors)
                    {
                        var realtorDTO = _mapper.Map<RealtorDTO>(realtor);
                        realtorDtos.Add(realtorDTO);
                    }

                    return Ok(realtorDtos);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // GET method that returns a Realtor object stored in the DB based on Id.
        // GET api/<RealtorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RealtorDTO>> GetRealtor(string id)
        {
            try
            {
                var realtor = await _realtorRepository.GetRealtorByIdAsync(id);
                if (realtor == null)
                {
                    return NotFound();
                }

                var realtorDto = _mapper.Map<RealtorDTO>(realtor);

                return Ok(realtorDto);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }

        }


        // Added from Repository 2024-04-24 Reb
        // GET method that returns a Realtor object stored in the DB based on Id.
        // GET api/<RealtorController>/by-agency/{string}
        [HttpGet("by-agency/{agencyName}")]
        public async Task<ActionResult<List<RealtorDTO>>> GetRealtorsByAgency(string agencyName)
        {
            try
            {
                var agency = await _realtorRepository.GetAgencyByNameAsync(agencyName);
                if (agency == null)
                {
                    return NotFound();
                }

                var realtors = await _realtorRepository.GetRealtorsByAgencyAsync(agency);

                List<RealtorDTO> realtorDtos = new();
                foreach (var realtor in realtors)
                {
                    var realtorDto = _mapper.Map<RealtorDTO>(realtor);
                    realtorDtos.Add(realtorDto);
                }

                return Ok(realtorDtos);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }

        }

        [HttpPost]
        [Authorize(Roles = ApiRoles.Admin)]
        public async Task<ActionResult> PostRealtor(RealtorDTO realtorDto)
        {
            try
            {
                var realtor = _mapper.Map<Realtor>(realtorDto);

                await _realtorRepository.AddRealtorAsync(realtor);

                return CreatedAtAction(nameof(GetRealtor), new { id = realtor.Id }, realtor);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // PUT method that updates an existing Realtor object in the DB based on Id and Realtor object.
        // PUT api/<RealtorController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Realtor, Admin")]
        public async Task<ActionResult<RealtorDTO>> PutRealtor(RealtorDTO realtorDto)
        {
            //if (id != realtorDto.Id)
            //{
            //    return BadRequest();
            //}
            try
            {
                var existingRealtor = await _realtorRepository.GetRealtorByIdAsync(realtorDto.Id);
                if (existingRealtor == null)
                {
                    return NotFound();
                }

                // Fungerande lösning. Kolla AutoMapper profil.
                existingRealtor.FirstName = realtorDto.FullName.Split(' ')[0];
                existingRealtor.LastName = realtorDto.FullName.Split(' ')[1];
                existingRealtor.Email = realtorDto.Email;
                existingRealtor.NormalizedEmail = realtorDto.Email.ToUpper();
                existingRealtor.PhoneNumber = realtorDto.PhoneNumber;

                var updatedRealtor = await _realtorRepository.UpdateRealtorAsync(existingRealtor);
                return Ok(updatedRealtor);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // DELETE method that finds and deletes an existing Realtor object based on Id.
        // DELETE api/<RealtorController>/5
        [HttpDelete("{realtorId}/{newRealtorId}")]
        [Authorize(Roles = ApiRoles.Admin)]
        public async Task<ActionResult> DeleteRealtor(string realtorId, string newRealtorId)
        {
            try
            {
                var realtor = await _realtorRepository.GetRealtorByIdAsync(realtorId);
                if (realtor == null)
                {
                    return NotFound();
                }
                var newRealtor = await _realtorRepository.GetRealtorByIdAsync(newRealtorId);
                await _realtorRepository.DeleteRealtorAsync(realtor, newRealtor);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergHomes.API.Data;
using FribergHomes.API.Models;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Mappers;
using AutoMapper;
using FribergHomes.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using FribergHomes.API.Constants;

namespace FribergHomes.API.Controllers
{

    //Author: Sanna 2024-04-16
    //@ Updates: Added error handling / Sanna 2024-04-19 
    //@ Updates: GET methods now returns DTOs instead of models / Sanna 2024-04-24
    //@ Update: Added GetRealtorsAtAgency(int id) / Reb 2024-05-02
    //@ Update: Implemented Authorize-attributes on selected endpoints /Tobias 2024-05-20

    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgency _agencyRepository;
        private readonly IMapper _mapper;
        private readonly string _generalFaultMessage = "Ett oväntat fel uppstod vid hanteringen av förfrågan!";

        public AgencyController(IAgency agencyRepository, IMapper mapper)
        {
            _agencyRepository = agencyRepository;
            _mapper = mapper;
        }

        // GET: api/Agency
        [HttpGet]
        public async Task<ActionResult<List<AgencyDTO>>> GetAgencies()
        {
            try
            {
                var agencies = await _agencyRepository.GetAllAgenciesAsync();
                if (agencies == null)
                {
                    return NotFound("Det finns inga mäklarbyråer.");
                }

                List<AgencyDTO> agencyDTOs = new();
                foreach(var agency in agencies)
                {
                    var agencyDTO = _mapper.Map<AgencyDTO>(agency);
                    agencyDTOs.Add(agencyDTO);
                }
                
                return Ok(agencyDTOs);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // GET: api/Agency/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgencyDTO>> GetAgency(int id)
        {
            try
            {
                var agency = await _agencyRepository.GetAgencyByIdAsync(id);
                if (agency == null)
                {
                    return NotFound($"Det existerar ingen mäklarbyrå med ID {id}.");
                }
                var agencyDTO = _mapper.Map<AgencyDTO>(agency);
                return Ok(agencyDTO);
               
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }


        /* GET all realtors at agency */
        [HttpGet("realtors/{id}")]
        public async Task<ActionResult<List<RealtorDTO>>> GetRealtorsAtAgency(int id)
        {
            try
            {
                var agency = await _agencyRepository.GetAgencyByIdAsync(id);
                if (agency == null)
                {
                    return NotFound($"Det existerar ingen mäklarbyrå med ID {id}.");
                }

                List<RealtorDTO> realtorsDtos = new List<RealtorDTO>();
                var realtors = await _agencyRepository.GetRealtorsAtAgencyAsync(agency.Id);
                foreach (var realtor in realtors)
                { 
                    var realtorDto = _mapper.Map<RealtorDTO>(realtor);
                    realtorsDtos.Add(realtorDto);
                }
                return Ok(realtorsDtos);

            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }



        /* TODO: Ska hämta agency med realtors-email*/
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<AgencyDTO>> GetAgencyByEmail(string email)
        {
            try
            {
                var agency = await _agencyRepository.GetAgencyByRealtorEmail(email);
                if (agency == null)
                {
                    return NotFound($"Det existerar ingen mäklarbyrå med ID {email}.");
                }

                var agencyDTO = _mapper.Map<AgencyDTO>(agency);
                return Ok(agencyDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, _generalFaultMessage + e.Message);
            }
        }

        /*Hämtar salesobject i ddenna agency*/
        [HttpGet("saleobjects/{id}")]
        public async Task<ActionResult<AgencyDTO>> GetSalesAtAgency(int id)
        {
            try
            {
                var saleObjects = await _agencyRepository.GetSalesObjectsAtAgencyAsync(id);
                if (saleObjects == null)
                {
                    return NotFound($"Det existerar inga annonser i denna byrå.");
                }

                List<SalesObjectDTO> saleObjectsDtos = new List<SalesObjectDTO>();
                foreach (var so in saleObjects)
                {
                    var salesObjectDto = _mapper.Map<SalesObjectDTO>(so);
                    saleObjectsDtos.Add(salesObjectDto);
                }
                return Ok(saleObjectsDtos);

            }
            catch (Exception e)
            {
                return StatusCode(500, _generalFaultMessage + e.Message);
            }
        }




        // PUT: api/Agency/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754       

        [HttpPut("{id}")]
        [Authorize(Roles = ApiRoles.Admin)]
        public async Task<IActionResult> PutAgency(int id, AgencyDTO agencyDTO)
        {
            try
            {
                if (id != agencyDTO.Id)
                {
                    return BadRequest($"Ingen mäklarbyrå har ID {id}.");
                }
                var agency = _mapper.Map<Agency>(agencyDTO);
                var updatedAgency = await _agencyRepository.UpdateAgencyAsync(agency);
                if (updatedAgency == null)
                {
                    return NotFound($"Mäklarbyrån du försökte uppdatera existerar inte.");
                }
                return Ok(updatedAgency);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // POST: api/Agency
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = ApiRoles.Admin)]
        public async Task<ActionResult<AgencyDTO>> PostAgency(AgencyDTO agencyDTO)
        {
            try
            {
                var agency = _mapper.Map<Agency>(agencyDTO);
                await _agencyRepository.AddAgencyAsync(agency);
                return CreatedAtAction("GetAgency", new { id = agency.Id }, agency);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // DELETE: api/Agency/5             
        [HttpDelete("{id}")]
        [Authorize(Roles = ApiRoles.Admin)]
        public async Task<IActionResult> DeleteAgency(int id)
        {
            try
            {
                var agency = await _agencyRepository.GetAgencyByIdAsync(id);
                if (agency == null)
                {
                    return NotFound($"Mäklarbyrån du försökte radera existerar inte.");
                }
                await _agencyRepository.DeleteAgencyAsync(agency.Id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

    }
}

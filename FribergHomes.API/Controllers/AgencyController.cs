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

namespace FribergHomes.API.Controllers
{

    //Author: Sanna 2024-04-16
    //Added error handling 2024-04-19

    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgency _agencyRepository;
        private readonly string _generalFaultMessage = "Ett oväntat fel uppstod vid hanteringen av förfrågan!";

        public AgencyController(IAgency agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        // GET: api/Agency
        [HttpGet]
        public async Task<ActionResult<List<Agency>>> GetAgencies()
        {
            try
            {

                var agencies = await _agencyRepository.GetAllAgenciesAsync();
                if (agencies == null)
                {
                    return NotFound("Inga kommuner hittades.");
                }
                return Ok(agencies);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // GET: api/Agency/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agency>> GetAgency(int id)
        {
            try
            {
                var agency = await _agencyRepository.GetAgencyByIdAsync(id);
                if (agency == null)
                {
                    return NotFound($"Tyvärr existerar det ingen mäklarfirma med id {id}. :(");
                }
                return Ok(agency);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // PUT: api/Agency/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754       

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgency(int id, Agency agency)
        {
            try
            {
                if (id != agency.Id)
                {
                    return BadRequest($"Det finns ingen mäklarfirma som matchar med ID {id}");
                }
                var updatedAgency = await _agencyRepository.UpdateAgencyAsync(agency);
                if (updatedAgency == null)
                {
                    return NotFound($"{agency.Name} verkar inte existera.");
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
        public async Task<ActionResult<Agency>> PostAgency(Agency agency)
        {
            try
            {
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
        public async Task<IActionResult> DeleteAgency(int id)
        {
            try
            {
                var realtor = await _agencyRepository.GetAgencyByIdAsync(id);
                if (realtor == null)
                {
                    return NotFound($"Det existerar ingen mäklarfirma med id {id}");
                }
                await _agencyRepository.DeleteAgencyAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

    }
}

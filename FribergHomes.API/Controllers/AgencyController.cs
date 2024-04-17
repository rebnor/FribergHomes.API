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

    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgency _agencyRepository;

        public AgencyController(IAgency agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        // GET: api/Agency
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agency>>> GetAgencies()
        {
            return await _agencyRepository.GetAllAgenciesAsync();
        }

        // GET: api/Agency/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agency>> GetAgency(int id)
        {
            var agency = await _agencyRepository.GetAgencyByIdAsync(id);
            if (agency == null)
            {
                return NotFound();
            }
            return agency;
        }

        // PUT: api/Agency/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // @ Created the varieable updatedAgency to make sure the updated object that returns from the repo has a value

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgency(int id, Agency agency)
        {
            if (id != agency.Id)
            {
                return BadRequest();
            }
            var updatedAgency = await _agencyRepository.UpdateAgencyAsync(agency);
            if (updatedAgency == null)
            {
                return NotFound();
            }
            return Ok(updatedAgency);
        }

        // POST: api/Agency
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agency>> PostAgency(Agency agency)
        {
            await _agencyRepository.AddAgencyAsync(agency);
            return CreatedAtAction("GetAgency", new { id = agency.Id }, agency);
        }

        // DELETE: api/Agency/5             
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgency(int id)
        {
            var realtor = await _agencyRepository.GetAgencyByIdAsync(id);
            if (realtor == null)
            {
                return NotFound();
            }
            await _agencyRepository.DeleteAgencyAsync(id);

            //the NoContent return lets the client know that the agency has been deleted
            return NoContent();
        }

    }
}

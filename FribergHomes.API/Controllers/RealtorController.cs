using FribergHomes.API.Data;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Mvc;


namespace FribergHomes.API.Controllers
{

     /* API controller to handle HTTP requests and responses related to Realtor objects.
      * Author: Tobias 2024-04-16
      */

    [Route("api/[controller]")]
    [ApiController]
    public class RealtorController : ControllerBase
    {
        private readonly IRealtor _realtorRepository;

        public RealtorController(IRealtor realtorRepository)
        {
            _realtorRepository = realtorRepository;
        }

        // GET method that returns a list of all Realtor objects stored in the DB.
        // GET: api/<RealtorController>
        [HttpGet]
        public async Task<ActionResult<List<Realtor>>> GetRealtors()
        {
            var realtors = await _realtorRepository.GetAllRealtorsAsync();
            return Ok(realtors);
        }

        // GET method that returns a Realtor object stored in the DB based on Id.
        // GET api/<RealtorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Realtor>> GetRealtor(int id)
        {
            var realtor = await _realtorRepository.GetRealtorByIdAsync(id);
            if(realtor == null)
            {
                return NotFound();
            }
            return Ok(realtor);
        }

        // POST method that creates and stores a Realtor object in the DB.
        // POST api/<RealtorController>
        [HttpPost]
        public async Task<ActionResult> PostRealtor(Realtor realtor)
        {
            await _realtorRepository.AddRealtorAsync(realtor);
            return CreatedAtAction(nameof(GetRealtor), new { id = realtor.Id }, realtor);
        }

        // PUT method that updates an existing Realtor object in the DB based on Id and Realtor object.
        // PUT api/<RealtorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Realtor>> PutRealtor(int id, Realtor realtor)
        {
            if(id != realtor.Id)
            {
                return BadRequest();
            }
            var existingRealtor = await _realtorRepository.GetRealtorByIdAsync(id);
            if (existingRealtor == null)
            {
                return NotFound();
            }
            await _realtorRepository.UpdateRealtorAsync(realtor);
            return Ok(realtor);
        }

        // DELETE method that finds and deletes an existing Realtor object based on Id.
        // DELETE api/<RealtorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRealtor(int id)
        {
            var realtor = await _realtorRepository.GetRealtorByIdAsync(id);
            if (realtor == null)
            {
                return NotFound();
            }
            await _realtorRepository.DeleteRealtorAsync(realtor);
            return Ok();
        }
    }
}

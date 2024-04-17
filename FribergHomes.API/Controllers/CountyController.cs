using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Mvc;


namespace FribergHomes.API.Controllers
{

     /* API controller to handle HTTP requests and responses related to County objects.
      * Author: Tobias 2024-04-17
      */

    [Route("api/[controller]")]
    [ApiController]
    public class CountyController : ControllerBase
    {
        private readonly ICounty _countyRepository;

        public CountyController(ICounty countyRepository)
        {
            _countyRepository = countyRepository;
        }


        // GET method that returns a list of all County objects stored in the DB.
        // GET: api/<RealtorController>
        [HttpGet]
        public async Task<ActionResult<List<County>>> GetCounties()
        {
            try
            {
                var counties = await _countyRepository.GetAllCountiesAsync();
                return Ok(counties);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ett oväntat fel uppstod vid hanteringen av förfrågan!");
            }
        }

        // GET method that returns a County object stored in the DB based on Id.
        // GET api/<RealtorController>/by-id/5
        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<County>> GetCounty(int id)
        {
            try
            {
                var county = await _countyRepository.GetCountyByIdAsync(id);
                if (county == null)
                {
                    return NotFound($"Kommun med ID: {id} ej funnen!");
                }
                return Ok(county);
            }
            catch(Exception)
            {
                return StatusCode(500, "Ett oväntat fel uppstod vid hanteringen av förfrågan!");
            }
        }

        // GET method that returns a County object stored in the DB based on name.
        // GET api/<RealtorController>/by-name/name
        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<County>> GetCounty(string name)
        {
            try
            {
                var county = await _countyRepository.GetCountyByNameAsync(name);
                if (county == null)
                {
                    return NotFound($"Kommun med namn: {name} ej funnen!");
                }
                return Ok(county);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ett oväntat fel uppstod vid hanteringen av förfrågan!");
            }
            
        }

        // POST method that creates and stores a County object in the DB.
        // POST api/<RealtorController>
        [HttpPost]
        public async Task<ActionResult> PostCounty(County county)
        {
            try
            {
                await _countyRepository.AddCountyAsync(county);
                return CreatedAtAction(nameof(GetCounty), new { id = county.Id }, county);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ett oväntat fel uppstod vid hanteringen av förfrågan!");
            }
            
        }

        // PUT method that finds and updates an existing County object in the DB based on Id and Realtor object.
        // PUT api/<RealtorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<County>> PutCounty(int id, County county)
        {
            if (id != county.Id)
            {
                return BadRequest($"Angivet ID stämmer ej överens med kommun-ID!");
            }
            try
            {
                var existingCounty = await _countyRepository.GetCountyByIdAsync(id);
                if (existingCounty == null)
                {
                    return NotFound($"Kommun med ID: {id} ej funnen!");
                }
                await _countyRepository.UpdateCountyAsync(county);
                return Ok(county);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ett oväntat fel uppstod vid hanteringen av förfrågan!");
            }


        }

        // DELETE method that finds and deletes an existing County object based on Id.
        // DELETE api/<RealtorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCounty(int id)
        {
            try
            {
                var county = await _countyRepository.GetCountyByIdAsync(id);
                if (county == null)
                {
                    return NotFound($"Kommun med ID: {id} ej funnen!");
                }
                await _countyRepository.DeleteCountyAsync(county);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ett oväntat fel uppstod vid hanteringen av förfrågan!");
            }
        }

    }
}

using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FribergHomes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesObjectController : ControllerBase
    {
        private readonly ISalesObject _salesRepo;

        public SalesObjectController(ISalesObject salesRepo)
        {
            _salesRepo = salesRepo;
        }

        // GET: api/<SalesObjectController>
        [HttpGet]
        public async Task<List<SalesObject>> GetAllSalesObjects()
        {
            var salesObjects = await _salesRepo.GetAllSalesObjectsAsync();
            return salesObjects;
        }

        // GET api/<SalesObjectController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesObjectById(int id)
        {
            var salesObject = await _salesRepo.GetSalesObjectByIdAsync(id);
            return Ok(salesObject);
        }

        // POST api/<SalesObjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesObject salesObject)
        {
            try
            {
                var addedSalesObject = await _salesRepo.AddSalesObjectAsync(salesObject);
                return CreatedAtAction(nameof(GetSalesObjectById), new { id = addedSalesObject.Id }, addedSalesObject);
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<SalesObjectController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<SalesObjectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

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
        public IActionResult GetAllSalesObjects()
        {
            var salesObjects = _salesRepo.GetAllSalesObjectsAsync();
            return Ok(salesObjects);
        }

        // GET api/<SalesObjectController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SalesObjectController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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

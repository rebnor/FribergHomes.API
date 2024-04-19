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
    /* Controller for SalesObject
    * @ Author: Rebecka 2024-04-17  
    * @ Updates: changed from ienumerable to list in GetSalesObjects() / Rebecka 2024-04-19
    */
    [Route("api/[controller]")]
    [ApiController]
    public class SalesObjectController : ControllerBase
    {
        private readonly ISalesObject _salesRepo;

        public SalesObjectController(ISalesObject salesRepo)
        {
            _salesRepo = salesRepo;
        }

        // GET: api/SalesObjects
        /* Gets All the SalesObjects from the Database */
        [HttpGet]
        public async Task<ActionResult<List<SalesObject>>> GetSalesObjects()
        {
            try
            {
                var salesObjects = await _salesRepo.GetAllSalesObjectsAsync();
                if (salesObjects == null)
                {
                    return NoContent();
                }
                return Ok(salesObjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av alla Försäljningsobjekt! Felmeddelande: {ex.Message}");
            }
        }

        // GET: api/SalesObjects/{id}
        /* Gets One SalesObject from Database, with int ID */
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesObject>> GetSalesObject(int id)
        {
            try
            {
                var salesObject = await _salesRepo.GetSalesObjectByIdAsync(id);
                if (salesObject == null)
                {
                    return NotFound();
                }
                return Ok(salesObject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av Försäljningsobjekt med id {id}! Felmeddelande: {ex.Message}");
            }
        }

        // PUT: api/SalesObjects/{id}
        /* Updates one SalesObject in the Database */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesObject(int id, SalesObject salesObject)
        {
            try
            {
                if (id != salesObject.Id)
                {
                    return BadRequest();
                }
                await _salesRepo.UpdateSalesObjectAsync(salesObject);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid uppdatering av Försäljningsobjekt med id {id}! Felmeddelande: {ex.Message}");
            }
        }

        // POST: api/SalesObjects
        /* Adds one SalesObject to the Database */
        [HttpPost]
        public async Task<ActionResult<SalesObject>> PostSalesObject(SalesObject salesObject)
        {
            try
            {
                await _salesRepo.AddSalesObjectAsync(salesObject);
                return CreatedAtAction("GetSalesObject", new { id = salesObject.Id }, salesObject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel när du skulle lägga till Försäljningsobjektet! Felmeddelande: {ex.Message}");
            }
        }

        // DELETE: api/SalesObjects/{id}
        /* Deletes one SalesObject, with int ID, from Database */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesObject(int id)
        {
            try
            {
                var salesObject = await _salesRepo.GetSalesObjectByIdAsync(id);
                if (salesObject != null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel när du skulle radera Fösäljningsobjekt med id '{id}'! Felmeddelande: {ex.Message}");
            }
        }
    }
}

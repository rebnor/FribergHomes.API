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
using FribergHomes.API.DTOs;

namespace FribergHomes.API.Controllers
{
    /* Controller for SalesObject
    * @ Author: Rebecka 2024-04-17  
    * @ Updates: changed from ienumerable to list in GetSalesObjects() / Rebecka 2024-04-19
    * @ Updates: Added conversion to SalesObjectDTO and List<SalesObjectDTO> and updated GET method return types to SalesObjectDTO.
    *            Added GET-method to retrieve all SalesObjects by County Id / Tobias 2024-04-23
    * @ Update: Changed to SalesObjectDTO in Put and Post, and using Mappers 
    *           Added the DeleteSalesObjectAsync in Delete-Method, it was already in Repository but never used / Reb 2024-04-25
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
        public async Task<ActionResult<List<SalesObjectDTO>>> GetSalesObjects()
        {
            try
            {
                var salesObjects = await _salesRepo.GetAllSalesObjectsAsync();
                if (salesObjects == null)
                {
                    return NoContent();
                }
                var salesObjectDTOs = DTOMapper.ToListSalesObjectDTO(salesObjects); // Tobias
                return Ok(salesObjectDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av alla Försäljningsobjekt! Felmeddelande: {ex.Message}");
            }
        }

        // GET: api/SalesObjects/{id}
        /* Gets One SalesObject from Database, with int ID */
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesObjectDTO>> GetSalesObject(int id)
        {
            try
            {
                var salesObject = await _salesRepo.GetSalesObjectByIdAsync(id);
                if (salesObject == null)
                {
                    return NotFound();
                }
                var salesObjectDTO = DTOMapper.ToSalesObjectDTO(salesObject);
                return Ok(salesObjectDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av Försäljningsobjekt med id {id}! Felmeddelande: {ex.Message}");
            }
        }

        // Gets all GetSalesObjects by county id from database. // Tobias 2024-04-23
        // GET: api/SalesObjects/county/{id}
        [HttpGet("county/{id}")]
        public async Task<ActionResult<List<SalesObjectDTO>>> GetSalesObjects(int id)
        {
            try
            {
                var salesObjects = await _salesRepo.GetSalesObjectsByCountyAsync(id);
                if (salesObjects == null)
                {
                    return NoContent();
                }
                var salesObjectDTOs = DTOMapper.ToListSalesObjectDTO(salesObjects);
                return Ok(salesObjectDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av försäljningsobjekt! Felmeddelande: {ex.Message}");
            }
        }

        // PUT: api/SalesObjects/{id}
        /* Updates one SalesObject in the Database */
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSalesObject(int id, SalesObject salesObject)
        //{
        //    try
        //    {
        //        if (id != salesObject.Id)
        //        {
        //            return BadRequest();
        //        }
        //        await _salesRepo.UpdateSalesObjectAsync(salesObject);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Något gick fel vid uppdatering av Försäljningsobjekt med id {id}! Felmeddelande: {ex.Message}");
        //    }
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult<SalesObjectDTO>> PutSalesObject(int id, SalesObjectDTO salesObjectDto)
        {
            try
            {
                if (id != salesObjectDto.Id)
                {
                    return BadRequest();
                }

                var salesObject = ModelMapper.DtoToSalesObject(salesObjectDto);
                var county = await _salesRepo.GetCountyByNameAsync(salesObjectDto.CountyName);
                if (salesObjectDto.CountyName == county.Name)
                {
                    salesObject.County = county;
                }

                var updatedSalesObject = await _salesRepo.UpdateSalesObjectAsync(salesObject);

                var dtoSalesObject = DTOMapper.ToSalesObjectDTO(updatedSalesObject);

                return Ok(dtoSalesObject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid uppdatering av Försäljningsobjekt med id {id}! Felmeddelande: {ex.Message}");
            }
        }

        // POST: api/SalesObjects
        /* Adds one SalesObject to the Database */
        //[HttpPost]
        //public async Task<ActionResult<SalesObject>> PostSalesObject(SalesObject salesObject)
        //{
        //    try
        //    {
        //        await _salesRepo.AddSalesObjectAsync(salesObject);
        //        return CreatedAtAction("GetSalesObject", new { id = salesObject.Id }, salesObject);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Något gick fel när du skulle lägga till Försäljningsobjektet! Felmeddelande: {ex.Message}");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<SalesObjectDTO>> PostSalesObject(SalesObjectDTO salesObjectDto)
        {
            try
            {
                var salesObject = ModelMapper.DtoToSalesObject(salesObjectDto);
                var county = await _salesRepo.GetCountyByNameAsync(salesObjectDto.CountyName);
                if (salesObjectDto.CountyName == county.Name)
                {
                    salesObject.County = county;
                }
                var addedSalesObject = await _salesRepo.AddSalesObjectAsync(salesObject);

                var dtoSalesObject = DTOMapper.ToSalesObjectDTO(addedSalesObject);

                return CreatedAtAction("GetSalesObject", new { id = salesObject.Id }, dtoSalesObject);
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
                await _salesRepo.DeleteSalesObjectAsync(salesObject);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel när du skulle radera Fösäljningsobjekt med id '{id}'! Felmeddelande: {ex.Message}");
            }
        }
    }
}

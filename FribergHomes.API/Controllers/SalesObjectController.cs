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
using AutoMapper;

namespace FribergHomes.API.Controllers
{
    /* Controller for SalesObject
    * @ Author: Rebecka 2024-04-17  
    * @ Updates: changed from ienumerable to list in GetSalesObjects() / Rebecka 2024-04-19
    * @ Updates: Added conversion to SalesObjectDTO and List<SalesObjectDTO> and updated GET method return types to SalesObjectDTO.
    *            Added GET-method to retrieve all SalesObjects by County Id / Tobias 2024-04-23

    * @ Update: Changed to SalesObjectDTO in Put and Post, and using Mappers  / Reb 2024-04-25

    * @ Updates: Updated Delete method (corrected salesObject null check (was checking for not null) and 
    *            added missing repository method call to erase object) / Tobias 2024-04-24

    @ Updates: Implemented AutoMapper (injection of IMapper). Revised POST-method.
               Added GetSalesObjectsByRealtor GET-end point / Tobias 2024-04-28.
    @ Update: Added GetSalesObjectsByCategory(int id) / Reb 2024-05-02
    */
    [Route("api/[controller]")]
    [ApiController]
    public class SalesObjectController : ControllerBase
    {
        private readonly ISalesObject _salesRepo;
        private readonly IMapper _mapper;

        public SalesObjectController(ISalesObject salesRepo, IMapper mapper)
        {
            _salesRepo = salesRepo;
            _mapper = mapper;
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

                List<SalesObjectDTO> salesObjectDTOs = new();
                foreach (var salesObject in salesObjects)
                {
                    var salesObjectDTO = _mapper.Map<SalesObjectDTO>(salesObject);
                    salesObjectDTOs.Add(salesObjectDTO);
                }
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
                var salesObjectDTO = _mapper.Map<SalesObjectDTO>(salesObject);
                return Ok(salesObjectDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av Försäljningsobjekt med id {id}! Felmeddelande: {ex.Message}");
            }
        }

        // Gets all GetSalesObjects by county id from database. // Tobias 2024-04-23
        // GET: api/SalesObject/county/{id}
        [HttpGet("county/{id}")]
        public async Task<ActionResult<List<SalesObjectDTO>>> GetSalesObjectsByCounty(int id)
        {
            try
            {
                var salesObjects = await _salesRepo.GetSalesObjectsByCountyAsync(id);

                if (salesObjects == null)
                {
                    return NoContent();
                }

                List<SalesObjectDTO> salesObjectDTOs = new();
                foreach (var salesObject in salesObjects)
                {
                    var salesObjectDTO = _mapper.Map<SalesObjectDTO>(salesObject);
                    salesObjectDTOs.Add(salesObjectDTO);
                }

                return Ok(salesObjectDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av försäljningsobjekt! Felmeddelande: {ex.Message}");
            }
        }

        // Gets all GetSalesObjects by realtor id from database. // Tobias 2024-04-29
        // GET: api/SalesObject/realtor/{id}
        [HttpGet("realtor/{id}")]
        public async Task<ActionResult<List<SalesObjectDTO>>> GetSalesObjectsByRealtor(int id)
        {
            try
            {
                var salesObjects = await _salesRepo.GetSalesObjectsByRealtorAsync(id);

                if (salesObjects == null)
                {
                    return NoContent();
                }

                List<SalesObjectDTO> salesObjectDTOs = new();

                foreach (var salesObject in salesObjects)
                {
                    var salesObjectDTO = _mapper.Map<SalesObjectDTO>(salesObject);
                    salesObjectDTOs.Add(salesObjectDTO);
                }

                return Ok(salesObjectDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av försäljningsobjekt! Felmeddelande: {ex.Message}");
            }
        }




        /********TEST**************/
        [HttpGet("county-name/{name}")]
        public async Task<ActionResult<List<SalesObjectDTO>>> GetSalesObjects(string name)
        {
            try
            {
                var salesObjects = await _salesRepo.GetSalesObjectsByCountyNameAsync(name);
                if (salesObjects == null)
                {
                    return NoContent();
                }


                //var salesObjectDTOs = DTOMapper.ToListSalesObjectDTO(salesObjects);

                List<SalesObjectDTO> salesObjectDTOs = new();

                foreach (var salesObject in salesObjects)
                {
                    var salesObjectDTO = _mapper.Map<SalesObjectDTO>(salesObject);            

                    salesObjectDTOs.Add(salesObjectDTO);

                }
                return Ok(salesObjectDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av försäljningsobjekt! Felmeddelande: {ex.Message}");
            }
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<List<SalesObjectDTO>>> GetSalesObjectsByCategory(int id)
        {
            try
            {
                var salesObjects = await _salesRepo.GetSalesObjectsByCategoryAsync(id);
                if (salesObjects == null)
                {
                    return NoContent();
                }
                //var salesObjectDTOs = DTOMapper.ToListSalesObjectDTO(salesObjects);
                List<SalesObjectDTO> salesObjectDTOs = new();

                foreach (var salesObject in salesObjects)
                {
                    var salesObjectDTO = _mapper.Map<SalesObjectDTO>(salesObject);
                    salesObjectDTOs.Add(salesObjectDTO);
                }

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

                var salesObject = _mapper.Map<SalesObject>(salesObjectDto);

                var updatedSalesObject = await _salesRepo.UpdateSalesObjectAsync(salesObject);

                var updatedSalesObjectDto = _mapper.Map<SalesObjectDTO>(updatedSalesObject);

                //var salesObject = ModelMapper.DtoToSalesObject(salesObjectDto);

                //var county = await _salesRepo.GetCountyByNameAsync(salesObjectDto.CountyName);
                //if (salesObjectDto.CountyName == county.Name)
                //{
                //    salesObject.County = county;
                //}

                //var category = await _salesRepo.GetCategoryByNameAsync(salesObjectDto.CategoryName);
                //if (salesObjectDto.CategoryName == category.Name)
                //{
                //    salesObject.Category = category;
                //}

                //var updatedSalesObject = await _salesRepo.UpdateSalesObjectAsync(salesObject);

                //var dtoSalesObject = DTOMapper.ToSalesObjectDTO(updatedSalesObject);

                return Ok(updatedSalesObjectDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid uppdatering av Försäljningsobjekt med id {id}! Felmeddelande: {ex.Message}");
            }
        }

        // POST: api/SalesObject
        /* Creates and stores a SalesObject in the Database */
        [HttpPost]
        public async Task<ActionResult<SalesObjectDTO>> PostSalesObject(SalesObjectDTO salesObjectDto)
        {
            try
            {
                var salesObject = _mapper.Map<SalesObject>(salesObjectDto);

                await _salesRepo.AddSalesObjectAsync(salesObject);

                return CreatedAtAction("GetSalesObject", new { id = salesObject.Id }, salesObject);

                //var salesObject = ModelMapper.DtoToSalesObject(salesObjectDto);
                //var county = await _salesRepo.GetCountyByNameAsync(salesObjectDto.CountyName);
                //if (salesObjectDto.CountyName == county.Name)
                //{
                //    salesObject.County = county;
                //}
                //var addedSalesObject = await _salesRepo.AddSalesObjectAsync(salesObject);

                //var dtoSalesObject = DTOMapper.ToSalesObjectDTO(addedSalesObject);
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
                if (salesObject == null)
                {
                    return NotFound();
                }

                await _salesRepo.DeleteSalesObjectAsync(salesObject); // Tobias

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel när du skulle radera Fösäljningsobjekt med id '{id}'! Felmeddelande: {ex.Message}");
            }
        }
    }
}

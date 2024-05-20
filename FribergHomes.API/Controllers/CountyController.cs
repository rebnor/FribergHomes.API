using AutoMapper;
using FribergHomes.API.Constants;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FribergHomes.API.Controllers
{

    /* API controller to handle HTTP requests and responses related to County objects.
     * Author: Tobias 2024-04-17
     * Revised: Tobias 2024-04-18 Implemented generalFaultMessage for status code 500 responses.
     * @ Update: Switched from County to CountyDTO in methods 
     *         Added DTO&Model-Mapping where its needed / Reb 2024-04-25
     * Revised: Implemented Authorize-attributes on selected endpoints /Tobias 2024-05-20
     */

    [Route("api/[controller]")]
    [ApiController]
    public class CountyController : ControllerBase
    {
        private readonly string _generalFaultMessage = "Ett oväntat fel uppstod vid hanteringen av förfrågan!";
        private readonly ICounty _countyRepository;
        private readonly IMapper _mapper;

        public CountyController(ICounty countyRepository, IMapper mapper)
        {
            _countyRepository = countyRepository;
            _mapper = mapper;
        }


        // GET method that returns a list of all County objects stored in the DB.
        // GET: api/<RealtorController>
        [HttpGet]
        public async Task<ActionResult<List<CountyDTO>>> GetCounties()
        {
            try
            {
                var counties = await _countyRepository.GetAllCountiesAsync();

                List<CountyDTO> countyDTOs = new();
                foreach (var county in counties)
                {
                    var countyDTO = _mapper.Map<CountyDTO>(county);
                    countyDTOs.Add(countyDTO);
                }

                return Ok(countyDTOs);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // GET method that returns a County object stored in the DB based on Id.
        // GET api/<RealtorController>/by-id/5
        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<CountyDTO>> GetCounty(int id)
        {
            try
            {
                var county = await _countyRepository.GetCountyByIdAsync(id);

                if (county == null)
                {
                    return NotFound($"Kommun med ID: {id} ej funnen!");
                }

                var countyDTO = _mapper.Map<CountyDTO>(county);

                return Ok(countyDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // GET method that returns a County object stored in the DB based on name.
        // GET api/<RealtorController>/by-name/name
        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<CountyDTO>> GetCounty(string name)
        {
            try
            {
                var county = await _countyRepository.GetCountyByNameAsync(name);

                if (county == null)
                {
                    return NotFound($"Kommun med namn: {name} ej funnen!");
                }

                var countyDTO = _mapper.Map<CountyDTO>(county);

                return Ok(countyDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }

        }

        // POST method that creates and stores a County object in the DB.
        // POST api/<RealtorController>
        [HttpPost]
        [Authorize(Roles = ApiRoles.Admin)]
        public async Task<ActionResult> PostCounty(CountyDTO countyDTO)
        {
            try
            {
                var county = _mapper.Map<County>(countyDTO);

                await _countyRepository.AddCountyAsync(county);

                return CreatedAtAction(nameof(GetCounty), new { id = county.Id }, county);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = ApiRoles.Admin)]
        public async Task<ActionResult<CountyDTO>> PutCounty(int id, CountyDTO countyDto)
        {
            if (id != countyDto.Id)
            {
                return BadRequest($"Angivet ID stämmer ej överens med kommun-ID!");
            }

            var existingCounty = await _countyRepository.GetCountyByIdAsync(countyDto.Id);
            if (existingCounty == null)
            {
                return NotFound($"Kommun med ID: {id} ej funnen!");
            }

            try
            {
                var county = _mapper.Map<County>(countyDto);

                var updatedCounty = await _countyRepository.UpdateCountyAsync(county);

                var updatedCountyDto = _mapper.Map<CategoryDTO>(updatedCounty);

                return Ok(updatedCountyDto);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }


        }

        // DELETE method that finds and deletes an existing County object based on Id.
        // DELETE api/<RealtorController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = ApiRoles.Admin)]
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
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

    }
}

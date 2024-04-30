using FribergHomes.API.Data;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Mappers;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Mvc;
using FribergHomes.API.DTOs;


namespace FribergHomes.API.Controllers
{

    /* API controller to handle HTTP requests and responses related to Realtor objects.
     * Author: Tobias 2024-04-16
     * Revised: Tobias 2024-04-18 Implemented exception handling and
     * generalFaultMessage for status code 500 responses.
     * @ Update: Switched from Realtor-object to RealtorDTO-object 
     *          Added DtoToRealtor in Post & Put methods 
     *          Added GetRealtorByAgency, it was already in Repository but wasnt used here // Reb 2024-04-24
     * 
     */

    [Route("api/[controller]")]
    [ApiController]
    public class RealtorController : ControllerBase
    {
        private readonly string _generalFaultMessage = "Ett oväntat fel uppstod vid hanteringen av förfrågan!";
        private readonly IRealtor _realtorRepository;

        public RealtorController(IRealtor realtorRepository)
        {
            _realtorRepository = realtorRepository;
        }

        // GET method that returns a list of all Realtor objects stored in the DB.
        // GET: api/<RealtorController>
        [HttpGet]
        public async Task<ActionResult<List<RealtorDTO>>> GetRealtors()
        {
            try
            {
                var realtors = await _realtorRepository.GetAllRealtorsAsync();
                if (realtors == null)
                {
                    return NotFound();
                }
                var realtorsDtos = DTOMapper.MapRealtorListToDto(realtors);
                return Ok(realtorsDtos);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // GET method that returns a Realtor object stored in the DB based on Id.
        // GET api/<RealtorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RealtorDTO>> GetRealtor(int id)
        {
            try
            {
                var realtor = await _realtorRepository.GetRealtorByIdAsync(id);
                if (realtor == null)
                {
                    return NotFound();
                }
                var realtorDto = DTOMapper.MapRealtorToDto(realtor);
                return Ok(realtorDto);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }

        }


        // Added from Repository 2024-04-24 Reb
        // GET method that returns a Realtor object stored in the DB based on Id.
        // GET api/<RealtorController>/by-agency/{string}
        [HttpGet("by-agency/{agencyName}")]
        public async Task<ActionResult<List<RealtorDTO>>> GetRealtorsByAgency(string agencyName)
        {
            try
            {
                var agency = await _realtorRepository.GetAgencyByNameAsync(agencyName);
                if (agency == null)
                {
                    return NotFound();
                }

                var realtors = await _realtorRepository.GetRealtorsByAgencyAsync(agency);
                var dtoList = DTOMapper.MapRealtorListToDto(realtors);
                return Ok(dtoList);

            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }

        }




        // POST method that creates and stores a Realtor object in the DB.
        // POST api/<RealtorController>

        //Old Code
        //[HttpPost]
        //public async Task<ActionResult> PostRealtor(Realtor realtor)
        //{
        //    try
        //    {
        //        await _realtorRepository.AddRealtorAsync(realtor);
        //        return CreatedAtAction(nameof(GetRealtor), new { id = realtor.Id }, realtor);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, _generalFaultMessage);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> PostRealtor(RealtorDTO realtorDto)
        {
            try
            {

                var realtor = ModelMapper.DtoToRealtor(realtorDto);
                var agency = await _realtorRepository.GetAgencyByNameAsync(realtorDto.Agency);
                if (realtorDto.Agency == agency.Name)
                {
                    realtor.Agency = agency;
                }

                var addedRealtor = await _realtorRepository.AddRealtorAsync(realtor);

                var dtoRealtor = DTOMapper.MapRealtorToDto(addedRealtor);

                return CreatedAtAction(nameof(GetRealtor), new { id = realtor.Id }, dtoRealtor);
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }

        // PUT method that updates an existing Realtor object in the DB based on Id and Realtor object.
        // PUT api/<RealtorController>/5

        //Old Code
        //[HttpPut("{id}")]
        //public async Task<ActionResult<Realtor>> PutRealtor(int id, Realtor realtor)
        //{
        //    if(id != realtor.Id)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var existingRealtor = await _realtorRepository.GetRealtorByIdAsync(id);
        //        if (existingRealtor == null)
        //        {
        //            return NotFound();
        //        }
        //        await _realtorRepository.UpdateRealtorAsync(realtor);
        //        return Ok(realtor);
        //    }

        //    catch (Exception)
        //    {
        //        return StatusCode(500, _generalFaultMessage);
        //    }
        //}

        // PUT method that updates an existing Realtor object in the DB based on Id and Realtor object.
        // PUT api/<RealtorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<RealtorDTO>> PutRealtor(RealtorDTO realtorDto)
        {
            try
            {
                var realtor = ModelMapper.DtoToRealtor(realtorDto);
                var agency = await _realtorRepository.GetAgencyByNameAsync(realtorDto.Agency);
                if (realtorDto.Agency == agency.Name)
                {
                    realtor.Agency = agency;
                }

                //var salesObjects = await _realtorRepository.GetRealtorsSalesObjects(realtor);

                var updatedRealtor = await _realtorRepository.UpdateRealtorAsync(realtor);

                var dtoRealtor = DTOMapper.MapRealtorToDto(updatedRealtor);

                return Ok(dtoRealtor);
            }

            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }



        // DELETE method that finds and deletes an existing Realtor object based on Id.
        // DELETE api/<RealtorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRealtor(int id)
        {
            try
            {
                var realtor = await _realtorRepository.GetRealtorByIdAsync(id);
                if (realtor == null)
                {
                    return NotFound();
                }
                await _realtorRepository.DeleteRealtorAsync(realtor);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, _generalFaultMessage);
            }
        }
    }
}

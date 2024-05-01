using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergHomes.API.Data;
using FribergHomes.API.Models;
using FribergHomes.API.DTOs;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Mappers;
using AutoMapper;

namespace FribergHomes.API.Controllers
{
    /* Controller for Category
     * @ Author: Rebecka 2024-04-16
     * @ Update: Added Model-DTO and DTO-Model conversions. Changed API-endpoint return types to DTO.  // Tobias 2024-04-25
     */
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryController(ICategory categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // GET: api/Categories
        /* Gets All the Categories from the Database*/
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepo.GetAllCategoriesAsync();
                if (categories == null)
                {
                    return NoContent();
                }

                List<CategoryDTO> categoryDTOs = new();
                foreach(var category in categories)
                {
                    var categoryDTO = _mapper.Map<CategoryDTO>(category);
                    categoryDTOs.Add(categoryDTO);
                }

                return Ok(categoryDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av alla Kategorier! Felmeddelande: {ex.Message}");
            }
        }

        // GET: api/Categories/{id}
        /* Gets One Category from Database, with int ID*/
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            try
            {
                var category = await _categoryRepo.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                
                var categoryDTO = _mapper.Map<CategoryDTO>(category);

                return Ok(categoryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid hämtning av Kategori med id {id}! Felmeddelande: {ex.Message}");
            }
        }

        // PUT: api/Categories/{id}
        /* Updates a Category in the Database */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO categoryDto)
        {
            try
            {
                if (id != categoryDto.Id)
                {
                    return BadRequest();
                }
                
                var category = _mapper.Map<Category>(categoryDto);
                await _categoryRepo.UpdateCategoryAsync(category);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel vid uppdatering av Kategori med id {id}! Felmeddelande: {ex.Message}");
            }
        }

        // POST: api/Categories
        /* Adds a Category to the Database */
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _categoryRepo.AddCategoryAsync(category);
                var addedCategoryDto = _mapper.Map<CategoryDTO>(category);
                //return CreatedAtAction("GetCategory", new { id = category.Id }, category);
                return Ok(addedCategoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel när du skulle lägga till Kategori '{categoryDto.Name}'! Felmeddelande: {ex.Message}");
            }
        }

        // DELETE: api/Categories/{id}
        /* Deletes one Category, with int ID, from Database */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepo.GetCategoryByIdAsync(id);
                if (category != null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Något gick fel när du skulle radera Kategori med id '{id}'! Felmeddelande: {ex.Message}");
            }
        }

    }
}

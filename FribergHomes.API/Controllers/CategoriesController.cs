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
    /* Controller for Category
     * @ Author: Rebecka 2024-04-16        
     */
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _categoryRepo;

        public CategoriesController(ICategory categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        // GET: api/Categories
        /* Gets All the Categories from the Database*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _categoryRepo.GetAllCategoriesAsync();
        }

        // GET: api/Categories/{id}
        /* Gets One Category from Database, with int ID*/
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        // PUT: api/Categories/{id}
        /* Updates a Category in the Database */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            await _categoryRepo.UpdateGategoryAsync(category);
            return NoContent();
        }

        // POST: api/Categories
        /* Adds a Category to the Database */
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _categoryRepo.AddGategoryAsync(category);
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/{id}
        /* Deletes one Category, with int ID, from Database */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id);
            await _categoryRepo.DeleteGategoryAsync(category);
            return NoContent();
        }

    }
}

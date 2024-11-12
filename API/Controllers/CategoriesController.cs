 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using DataProcessing.Models;
using API.Repositories;
using API.IRepositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoriesController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                return await _categoryRepo.GetAllCategories();

            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            try
            {
				return await _categoryRepo.GetCategoryById(id);

			}catch(Exception ex)
            {
                return Problem(ex.Message);
            }

		}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Category category)
        {
            try
            {
                await _categoryRepo.Update(category);
                await _categoryRepo.SaveChanges();
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            try
            {
                await _categoryRepo.Create(category);
                await _categoryRepo.SaveChanges();

            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                await _categoryRepo.Delete(id);
                await _categoryRepo.SaveChanges();

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }
    }
}

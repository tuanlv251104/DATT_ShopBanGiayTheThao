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
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepo _brandRepo;

        public BrandsController(IBrandRepo brandRepo)
        {
            _brandRepo = brandRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            try
            {
                return await _brandRepo.GetAllBrands();

            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {
            try
            {
				return await _brandRepo.GetBrandById(id);

			}catch(Exception ex)
            {
                return Problem(ex.Message);
            }

		}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(Brand Brand)
        {
            try
            {
                await _brandRepo.Update(Brand);
                await _brandRepo.SaveChanges();
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand Brand)
        {
            try
            {
                await _brandRepo.Create(Brand);
                await _brandRepo.SaveChanges();

            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            try
            {
                await _brandRepo.Delete(id);
                await _brandRepo.SaveChanges();

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }
    }
}

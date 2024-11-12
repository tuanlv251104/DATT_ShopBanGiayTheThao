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
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialRepo _materialRepo;

        public MaterialsController(IMaterialRepo materialRepo)
        {
            _materialRepo = materialRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            try
            {
                return await _materialRepo.GetAllMaterials();

            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterial(Guid id)
        {
            try
            {
				return await _materialRepo.GetMaterialById(id);

			}catch(Exception ex)
            {
                return Problem(ex.Message);
            }

		}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial(Material Material)
        {
            try
            {
                await _materialRepo.Update(Material);
                await _materialRepo.SaveChanges();
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }

        [HttpPost]
        public async Task<ActionResult<Material>> PostMaterial(Material Material)
        {
            try
            {
                await _materialRepo.Create(Material);
                await _materialRepo.SaveChanges();

            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(Guid id)
        {
            try
            {
                await _materialRepo.Delete(id);
                await _materialRepo.SaveChanges();
            
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }
    }
}

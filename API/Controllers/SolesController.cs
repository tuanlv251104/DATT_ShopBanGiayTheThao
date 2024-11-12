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
    public class SolesController : ControllerBase
    {
        private readonly ISoleRepo _soleRepo;

        public SolesController(ISoleRepo soleRepo)
        {
            _soleRepo = soleRepo;
        }

        // GET: api/Soles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sole>>> GetSoles()
        {
            try
            {
                return await _soleRepo.GetAllSole();

            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: api/Soles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sole>> GetSole(Guid id)
        {
            try
            {
				return await _soleRepo.GetSoleById(id);

			}catch(Exception ex)
            {
                return Problem(ex.Message);
            }

		}

        // PUT: api/Soles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSole(Sole sole)
        {
            try
            {
                await _soleRepo.Update(sole);
                await _soleRepo.SaveChanges();
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }

        // POST: api/Soles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sole>> PostSole(Sole sole)
        {
            try
            {
                await _soleRepo.Create(sole);
                await _soleRepo.SaveChanges();

            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }

        // DELETE: api/Soles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSole(Guid id)
        {
            try
            {
                await _soleRepo.Delete(id);
                await _soleRepo.SaveChanges();

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            return Content("Success!");
        }
       
    }
}

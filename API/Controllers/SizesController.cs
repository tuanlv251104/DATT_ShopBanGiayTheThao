using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using DataProcessing.Models;
using API.IRepositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeRepo _SizeRepo;

        public SizesController(ISizeRepo SizeRepo)
        {
            _SizeRepo = SizeRepo;
        }

        // GET: api/Sizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Size>>> GetSizes()
        {
            return await _SizeRepo.GetAllSize();
        }

        // GET: api/Sizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Size>> GetSize(Guid id)
        {
            return await _SizeRepo.GetSizeById(id);
        }

        // PUT: api/Sizes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSize(Size Size)
        {
            try
            {
                await _SizeRepo.Update(Size);
            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            await _SizeRepo.SaveChanges();
            return Content("Success!");
        }

        // POST: api/Sizes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Size>> PostSize(Size Size)
        {
            try
            {
                await _SizeRepo.Create(Size);
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            await _SizeRepo.SaveChanges();

            return CreatedAtAction("GetSize", new { id = Size.Id }, Size);
        }

        // DELETE: api/Sizes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSize(Guid id)
        {
            try
            {
                await _SizeRepo.Delete(id);
            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            await _SizeRepo.SaveChanges();

            return Content("Success!");
        }
    }
}

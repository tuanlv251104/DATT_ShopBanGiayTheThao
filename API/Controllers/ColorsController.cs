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
    public class ColorsController : ControllerBase
    {
        private readonly IColorRepo _colorRepo;

        public ColorsController(IColorRepo colorRepo)
        {
            _colorRepo = colorRepo;
        }

        // GET: api/Colors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
            return await _colorRepo.GetAllColor();
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColor(Guid id)
        {
            return await _colorRepo.GetColorById(id);
        }

        // PUT: api/Colors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(Color color)
        {
            try
            {
                await _colorRepo.Update(color);
            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            await _colorRepo.SaveChanges();
            return Content("Success!");
        }

        // POST: api/Colors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(Color color)
        {
            try
            {
                await _colorRepo.Create(color);
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }

            await _colorRepo.SaveChanges();

            return CreatedAtAction("GetColor", new { id = color.Id }, color);
        }

        // DELETE: api/Colors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(Guid id)
        {
            try
            {
                await _colorRepo.Delete(id);
            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            await _colorRepo.SaveChanges();

            return Content("Success!");
        }
    }
}

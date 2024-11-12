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
using API.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingUnitsController : ControllerBase
    {
        private readonly IShippingUnitRepos _shippingUnitRepos;

        public ShippingUnitsController(IShippingUnitRepos shippingUnitRepos)
        {
            _shippingUnitRepos = shippingUnitRepos;
        }
        // GET: api/ShippingUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingUnit>>> GetShippingUnits()
        {
            return await _shippingUnitRepos.GetAllShippingUnit();
        }

        // GET: api/ShippingUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingUnit>> GetShippingUnit(Guid id)
        {
          
            var shippingUnit = await _shippingUnitRepos.GetShippingUnitById(id);

            if (shippingUnit == null)
            {
                return NotFound();
            }

            return shippingUnit;
        }

        // PUT: api/ShippingUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingUnit(Guid id, ShippingUnit shippingUnit)
        {
            if (id != shippingUnit.ShippingUnitID)
            {
                return BadRequest();
            }
            try
            {
               await _shippingUnitRepos.update(shippingUnit);
                
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            await _shippingUnitRepos.SaveChanges();

            return Content("Success!");

        }

        // POST: api/ShippingUnits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShippingUnit>> PostShippingUnit(ShippingUnit shippingUnit)
        {
            try
            {
                await _shippingUnitRepos.create(shippingUnit);
                await _shippingUnitRepos.SaveChanges();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            await _shippingUnitRepos.SaveChanges();

            return Content("Success!");
        }

        // DELETE: api/ShippingUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingUnit(Guid id)
        {
            try
            {
                await _shippingUnitRepos.delete(id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            await _shippingUnitRepos.SaveChanges();

            return Content("Success!");

        }
    }
}

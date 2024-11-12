using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataProcessing.Models;
using View.Data;
using View.IServices;

namespace View.Controllers
{
    public class ShippingUnitsController : Controller
    {
        private readonly IShippingUnitServices _shippingUnitServices;

        public ShippingUnitsController(IShippingUnitServices shippingUnitServices)
        {
            _shippingUnitServices = shippingUnitServices;
        }



        // GET: ShippingUnits
        public async Task<IActionResult> Index()
        {
            var listShip = await _shippingUnitServices.GetAllShippingUnit();
            if (listShip == null) return View("listShip is null");
            return View(listShip);
        }

        // GET: ShippingUnits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingUnit = await _shippingUnitServices.GetShippingUnitById(id);
            if (shippingUnit == null)
            {
                return NotFound();
            }

            return View(shippingUnit);
        }

        //// GET: ShippingUnits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShippingUnits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShippingUnitID,Name,Phone,Email,Address,Website,Status")] ShippingUnit shippingUnit)
        {
            if (ModelState.IsValid)
            {
                shippingUnit.ShippingUnitID = Guid.NewGuid();
                await _shippingUnitServices.create(shippingUnit);
                return RedirectToAction(nameof(Index));
            }
            return View(shippingUnit);
        }

        // GET: ShippingUnits/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingUnit = await _shippingUnitServices.GetShippingUnitById(id);
            if (shippingUnit == null)
            {
                return NotFound();
            }
            return View(shippingUnit);
        }

        //// POST: ShippingUnits/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ShippingUnitID,Name,Phone,Email,Address,Website,Status")] ShippingUnit shippingUnit)
        {
            if (id != shippingUnit.ShippingUnitID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _shippingUnitServices.update(shippingUnit);
                return RedirectToAction(nameof(Index));
            }
            return View(shippingUnit);
        }

        // GET: ShippingUnits/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingUnit = await _shippingUnitServices.GetShippingUnitById(id);
            if (shippingUnit == null)
            {
                return NotFound();
            }

            return View(shippingUnit);
        }

        // POST: ShippingUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            await _shippingUnitServices.delete(id);

            return RedirectToAction(nameof(Index));
        }

        //private bool ShippingUnitExists(Guid id)
        //{
        //  return (_context.shippingUnits?.Any(e => e.ShippingUnitID == id)).GetValueOrDefault();
        //}
    }
}

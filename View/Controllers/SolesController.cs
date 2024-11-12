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
using View.Servicecs;
using View.ViewModel;

namespace View.Controllers
{
    public class SolesController : Controller
    {
        private readonly ISoleServices _soleServices;

        public SolesController(ISoleServices soleServices)
        {
            _soleServices = soleServices;
        }

        // GET: Soles
        public async Task<IActionResult> Index(int currentPage = 1, int rowsPerPage = 10)
        {
            var materials = await _soleServices.GetAllSoles();

            // Phân trang
            var totalSoles = materials.Count();
            var totalPages = (int)Math.Ceiling((double)totalSoles / rowsPerPage);
            var pagedSoles = materials.Skip((currentPage - 1) * rowsPerPage).Take(rowsPerPage).ToList();

            var viewModel = new SolesViewModel
            {
                soles = pagedSoles,
                sole = new Sole(),
            };

            ViewBag.CurrentPage = currentPage;
            ViewBag.RowsPerPage = rowsPerPage;
            ViewBag.TotalPages = totalPages;

            return View(viewModel);
        }

        // GET: Soles/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (_soleServices.GetSoleById(id) == null)
            {
                return NotFound();
            }

            var sole = await _soleServices.GetSoleById(id);
            return View(sole);
        }

        // GET: Soles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Soles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(SolesViewModel solesViewModel)
        {
            if (ModelState.IsValid)
            {
                solesViewModel.sole.Id = Guid.NewGuid();
                await _soleServices.Create(solesViewModel.sole);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Soles/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
			if (_soleServices.GetSoleById(id) == null)
			{
				return NotFound();
			}

			var sole = await _soleServices.GetSoleById(id);
			return View(sole);
        }

        // POST: Soles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, SolesViewModel solesViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _soleServices.Update(solesViewModel.sole);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingMaterial = await _soleServices.GetSoleById(solesViewModel.sole.Id);
                    if (existingMaterial == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                solesViewModel.soles = await _soleServices.GetAllSoles();
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Lỗi không sửa được");
        }

        // GET: Soles/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
			if (_soleServices.GetSoleById(id) == null)
			{
				return NotFound();
			}

			var sole = await _soleServices.GetSoleById(id);
			return View(sole);
        }

        // POST: Soles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_soleServices.GetAllSoles() == null)
            {
                return Problem("Entity set 'Sole' is null.");
            }
            await _soleServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var material = await _soleServices.GetSoleById(id);
            if (material == null)
            {
                return NotFound();
            }

            material.Status = !material.Status;
            await _soleServices.Update(material);

            return RedirectToAction("Index");
        }
    }
}

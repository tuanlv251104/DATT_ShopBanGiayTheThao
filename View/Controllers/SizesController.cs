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
    public class SizesController : Controller
    {
        private readonly ISizeServices _sizeServices;

        public SizesController(ISizeServices sizeServices)
        {
            _sizeServices = sizeServices;
        }

        // GET: Sizes
        public async Task<IActionResult> Index(int currentPage = 1, int rowsPerPage = 10)
        {
            var sizes = await _sizeServices.GetAllSizes();

            // Phân trang
            var totalSizes = sizes.Count();
            var totalPages = (int)Math.Ceiling((double)totalSizes / rowsPerPage);
            var pagedSizes = sizes.Skip((currentPage - 1) * rowsPerPage).Take(rowsPerPage).ToList();

            var viewModel = new SizeViewModel
            {
                Sizes = pagedSizes,
                NewSize = new Size(),
            };

            ViewBag.CurrentPage = currentPage;
            ViewBag.RowsPerPage = rowsPerPage;
            ViewBag.TotalPages = totalPages;

            return View(viewModel);
        }

        // GET: Sizes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (_sizeServices.GetAllSizes == null)
            {
                return NotFound();
            }

            var size = await _sizeServices.GetSizeById(id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // GET: Sizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SizeViewModel sizeViewModel)
        {
            if (ModelState.IsValid)
            {
                sizeViewModel.NewSize.Id = Guid.NewGuid();
                await _sizeServices.Create(sizeViewModel.NewSize);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Sizes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (_sizeServices.GetAllSizes() == null)
            {
                return NotFound();
            }

            var size = await _sizeServices.GetSizeById(id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SizeViewModel sizeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sizeServices.Update(sizeViewModel.NewSize);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingMaterial = await _sizeServices.GetSizeById(sizeViewModel.NewSize.Id);
                    if (existingMaterial == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                sizeViewModel.Sizes = await _sizeServices.GetAllSizes();
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Lỗi không sửa được");
        }

        // GET: Sizes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_sizeServices.GetAllSizes() == null)
            {
                return NotFound();
            }

            var size = await _sizeServices.GetSizeById(id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_sizeServices.GetAllSizes() == null)
            {
                return Problem("Entity set 'Size'  is null.");
            }
            
            await _sizeServices.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            // Tìm kiếm vật liệu theo ID
            var material = await _sizeServices.GetSizeById(id);
            if (material == null)
            {
                return NotFound();
            }

            material.Status = !material.Status;
            await _sizeServices.Update(material);

            return RedirectToAction("Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataProcessing.Models;
using View.IServices;
using View.Servicecs;
using View.ViewModel;

namespace View.Controllers
{
    public class ColorsController : Controller
    {
        private readonly IColorServices _colorServices;

        public ColorsController(IColorServices colorServices)
        {
            _colorServices = colorServices;
        }

        // GET: Colors
        public async Task<IActionResult> Index(int currentPage = 1, int rowsPerPage = 10)
        {
            var colorr = await _colorServices.GetAllColors();

            // Phân trang
            var totalColor = colorr.Count();
            var totalPages = (int)Math.Ceiling((double)totalColor / rowsPerPage);
            var pagedMaterials = colorr.Skip((currentPage - 1) * rowsPerPage).Take(rowsPerPage).ToList();

            var viewModel = new ColorViewModel
            {
                color = new Color(),
                colors = pagedMaterials
            };

            ViewBag.CurrentPage = currentPage;
            ViewBag.RowsPerPage = rowsPerPage;
            ViewBag.TotalPages = totalPages;

            return View(viewModel);
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (_colorServices.GetAllColors() == null)
            {
                return NotFound();
            }

            var color = await _colorServices.GetColorById(id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(ColorViewModel colorViewModel)
        {
            if (ModelState.IsValid)
            {
                colorViewModel.color.Id = Guid.NewGuid();
                await _colorServices.Create(colorViewModel.color);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (_colorServices.GetAllColors() == null)
            {
                return NotFound();
            }

            var color = await _colorServices.GetColorById(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ColorViewModel colorViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _colorServices.Update(colorViewModel.color);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingMaterial = await _colorServices.GetColorById(colorViewModel.color.Id);
                    if (existingMaterial == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                colorViewModel.colors = await _colorServices.GetAllColors();
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Lỗi không sửa được");
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_colorServices.GetColorById(id) == null)
            {
                return NotFound();
            }

            var color = await _colorServices.GetColorById(id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_colorServices.GetAllColors() == null)
            {
                return Problem("Entity set 'ViewContext.Color'  is null.");
            }

            await _colorServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            // Tìm kiếm vật liệu theo ID
            var colorr = await _colorServices.GetColorById(id);
            if (colorr == null)
            {
                return NotFound();
            }

            colorr.Status = !colorr.Status;
            await _colorServices.Update(colorr);

            return RedirectToAction("Index");
        }
    }
}

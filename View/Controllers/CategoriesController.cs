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
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int currentPage = 1, int rowsPerPage = 10)
        {
            var categories = await _categoryServices.GetAllCategories();

            // Phân trang
            var totalCategories = categories.Count();
            var totalPages = (int)Math.Ceiling((double)totalCategories / rowsPerPage);
            var pagedCategories = categories.Skip((currentPage - 1) * rowsPerPage).Take(rowsPerPage).ToList();

            var viewModel = new CategoriesViewModel
            {
                Categories = pagedCategories,
                Category = new Category(),
            };

            ViewBag.CurrentPage = currentPage;
            ViewBag.RowsPerPage = rowsPerPage;
            ViewBag.TotalPages = totalPages;

            return View(viewModel);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (_categoryServices.GetAllCategories() == null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(CategoriesViewModel categoriesViewModel)
        {
            if (ModelState.IsValid)
            {
                categoriesViewModel.Category.Id = Guid.NewGuid();
                await _categoryServices.Create(categoriesViewModel.Category);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (_categoryServices.GetAllCategories() == null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoriesViewModel categoriesViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryServices.Update(categoriesViewModel.Category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingMaterial = await _categoryServices.GetCategoryById(categoriesViewModel.Category.Id);
                    if (existingMaterial == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                categoriesViewModel.Categories = await _categoryServices.GetAllCategories();
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Lỗi không sửa được");
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_categoryServices.GetAllCategories() == null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_categoryServices.GetAllCategories() == null)
            {
                return Problem("Entity set 'Category'  is null.");
            }

            await _categoryServices.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var material = await _categoryServices.GetCategoryById(id);
            if (material == null)
            {
                return NotFound();
            }

            material.Status = !material.Status;
            await _categoryServices.Update(material);

            return RedirectToAction("Index");
        }
    }
}

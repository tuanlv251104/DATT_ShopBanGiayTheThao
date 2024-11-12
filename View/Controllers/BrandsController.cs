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
    public class BrandsController : Controller
    {
        private readonly IBrandServices _brandServices;

        public BrandsController(IBrandServices context)
        {
            _brandServices = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index(int currentPage = 1, int rowsPerPage = 10)
        {
            var materials = await _brandServices.GetAllBrands();

            // Phân trang
            var totalMaterials = materials.Count();
            var totalPages = (int)Math.Ceiling((double)totalMaterials / rowsPerPage);
            var pagedMaterials = materials.Skip((currentPage - 1) * rowsPerPage).Take(rowsPerPage).ToList();

            var viewModel = new BrandViewModel
            {
                brands = pagedMaterials,
                Brand = new Brand(),
            };

            ViewBag.CurrentPage = currentPage;
            ViewBag.RowsPerPage = rowsPerPage;
            ViewBag.TotalPages = totalPages;

            return View(viewModel);
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (_brandServices.GetAllBrands() == null)
            {
                return NotFound();
            }

            var brand = await _brandServices.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, BrandViewModel brandViewModel)
        {
            if (ModelState.IsValid)
            {
                brandViewModel.Brand.Id = Guid.NewGuid();
                await _brandServices.Create(brandViewModel.Brand);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (_brandServices.GetAllBrands() == null)
            {
                return NotFound();
            }

            var brand = await _brandServices.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BrandViewModel brandViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _brandServices.Update(brandViewModel.Brand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingMaterial = await _brandServices.GetBrandById(brandViewModel.Brand.Id);
                    if (existingMaterial == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                brandViewModel.brands = await _brandServices.GetAllBrands();
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Lỗi không sửa được");
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_brandServices.GetAllBrands() == null)
            {
                return NotFound();
            }

            var brand = await _brandServices.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_brandServices.GetAllBrands() == null)
            {
                return Problem("Entity set 'Brand'  is null.");
            }

            await _brandServices.Delete(id);

			return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var material = await _brandServices.GetBrandById(id);
            if (material == null)
            {
                return NotFound();
            }

            material.Status = !material.Status;
            await _brandServices.Update(material);

            return RedirectToAction("Index");
        }
    }
}

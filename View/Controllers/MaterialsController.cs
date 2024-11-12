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
using View.ViewModel;

namespace View.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly IMaterialServices _materialServices;

        public MaterialsController(IMaterialServices materialServices)
        {
            _materialServices = materialServices;
        }

        // GET: Materials
        public async Task<IActionResult> Index(int currentPage = 1, int rowsPerPage = 10)
        {
            var materials = await _materialServices.GetAllMaterials();

            // Phân trang
            var totalMaterials = materials.Count();
            var totalPages = (int)Math.Ceiling((double)totalMaterials / rowsPerPage);
            var pagedMaterials = materials.Skip((currentPage - 1) * rowsPerPage).Take(rowsPerPage).ToList();

            var viewModel = new MaterialViewModel
            {
                Materials = pagedMaterials,
                NewMaterial = new Material(),
            };

            ViewBag.CurrentPage = currentPage;
            ViewBag.RowsPerPage = rowsPerPage;
            ViewBag.TotalPages = totalPages;

            return View(viewModel);
        }

        // GET: Materials/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (_materialServices.GetAllMaterials() == null)
            {
                return NotFound();
            }

            var material = await _materialServices.GetMaterialById(id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Materials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(MaterialViewModel materialViewModel)
        {
            if (ModelState.IsValid)
            {
                materialViewModel.NewMaterial.Id = Guid.NewGuid();
                await _materialServices.Create(materialViewModel.NewMaterial);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (_materialServices.GetAllMaterials() == null)
            {
                return NotFound();
            }

            var material = await _materialServices.GetMaterialById(id);
            if (material == null)
            {
                return NotFound();
            }
            var model = new MaterialViewModel
            {
                NewMaterial = material
            };
            return View(material);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,MaterialViewModel materialViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _materialServices.Update(materialViewModel.NewMaterial);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingMaterial = await _materialServices.GetMaterialById(materialViewModel.NewMaterial.Id);
                    if (existingMaterial == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                materialViewModel.Materials = await _materialServices.GetAllMaterials();
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Lỗi không sửa được");
        }

        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_materialServices.GetAllMaterials() == null)
            {
                return NotFound();
            }

            var material = await _materialServices.GetMaterialById(id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_materialServices.GetAllMaterials() == null)
            {
                return Problem("Entity set 'Material'  is null.");
            }

            await _materialServices.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            // Tìm kiếm vật liệu theo ID
            var material = await _materialServices.GetMaterialById(id);
            if (material == null)
            {
                return NotFound();
            }

            material.Status = !material.Status;
            await _materialServices.Update(material);

            return RedirectToAction("Index");
        }
    }
}

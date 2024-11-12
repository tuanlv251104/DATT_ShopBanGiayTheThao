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
using Newtonsoft.Json;
using System.Drawing;
using Org.BouncyCastle.Asn1.Ocsp;

namespace View.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductServices _productServices;
		private readonly ISoleServices _soleServices;
		private readonly ICategoryServices _categoryServices;
		private readonly IBrandServices _brandServices;
		private readonly IMaterialServices _materialServices;
		private readonly IColorServices _colorServices;
		private readonly ISizeServices _sizeServices;
		private readonly IProductDetailService _productDetailService;
		private readonly IAccountService _accountService;
		private readonly IImageServices _imageServices;
		private readonly ISelectedImageServices _selectedImageServices;
		private readonly IEmailSender _emailSender;

		public ProductsController(IImageServices imageServices, ISelectedImageServices selectedImageServices, IProductServices productService, ISoleServices soleServices, ICategoryServices categoryServices, IBrandServices brandServices, IMaterialServices materialServices, IColorServices colorServices, ISizeServices sizeServices, IProductDetailService productDetailService, IEmailSender emailSender, IAccountService accountService)
		{
			_productServices = productService;
			_soleServices = soleServices;
			_categoryServices = categoryServices;
			_brandServices = brandServices;
			_materialServices = materialServices;
			_colorServices = colorServices;
			_sizeServices = sizeServices;
			_productDetailService = productDetailService;
			_accountService = accountService;
			_imageServices = imageServices;
			_selectedImageServices = selectedImageServices;
			_emailSender = emailSender;
		}

		// GET: Products
		public async Task<IActionResult> Index()
		{
			var viewContext = _productServices.GetAllProducts().Result;
			if (viewContext == null) return View("'Product is null!'");
			return View(viewContext.ToList());
		}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(Guid id)
		{
			var product = _productServices.GetProductById(id);
			return View(product);
		}

		// GET: Products/Create
		public IActionResult Create()
		{
			ViewData["BrandId"] = new SelectList(_brandServices.GetAllBrands().Result.Where(x => x.Status), "Id", "Name");
			ViewData["CategoryId"] = new SelectList(_categoryServices.GetAllCategories().Result.Where(x => x.Status), "Id", "Name");
			ViewData["MaterialId"] = new SelectList(_materialServices.GetAllMaterials().Result.Where(x => x.Status), "Id", "Name");
			ViewData["SoleId"] = new SelectList(_soleServices.GetAllSoles().Result.Where(x => x.Status), "Id", "TypeName");
			ViewData["ColorId"] = new SelectList(_colorServices.GetAllColors().Result.Where(x => x.Status), "Id", "Name");
			ViewData["SizeId"] = new SelectList(_sizeServices.GetAllSizes().Result.Where(x => x.Status), "Id", "Value");

			var dataForImage = new ProductAndDetailViewModel()
			{
				Images = _imageServices.GetAllImages().Result
			};
			return View(dataForImage);
		}

		// POST: Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductAndDetailViewModel viewModel, string productDetailsJson)
		{
			if (ModelState.IsValid)
			{
				if (string.IsNullOrEmpty(productDetailsJson))
				{
					ModelState.AddModelError("", "Product details are missing!");
					return View(viewModel);
				}

				var product = new Product
				{
					Id = Guid.NewGuid(),
					Name = viewModel.Name,
					Description = viewModel.Description,
					CategoryId = viewModel.CategoryId,
					SoleId = viewModel.SoleId,
					BrandId = viewModel.BrandId,
					MaterialId = viewModel.MaterialId
				};

				await _productServices.Create(product);
				var subscribedUsers = (await _accountService.GetAllCustomer())
								 .Where(u => u.IsSubscribedToNews)
								 .ToList();

				string emailSubject = "SẢN PHẨM MỚI HÓT HÒN HỌT ĐÂY !!!";
				string emailMessage = $"Sản phẩm {product.Name} mới được ra lò";
				foreach (var user in subscribedUsers)
				{
					await _emailSender.SendEmailAsync(user.Email, emailSubject, emailMessage);
				}
				var productDetails = JsonConvert.DeserializeObject<List<ProductDetailViewModel>>(productDetailsJson);
				var productid = product.Id;

				if (productDetails == null || !productDetails.Any())
				{
					ModelState.AddModelError("", "Product details are invalid or empty.");
					return View(viewModel);
				}

				// Lưu chi tiết sản phẩm
				foreach (var detail in productDetails)
				{
					var productDetail = new ProductDetail
					{
						Id = Guid.NewGuid().ToString(),
						ProductId = productid,
						Price = detail.Price,
						Stock = detail.Stock,
						Weight = detail.Weight,
						ColorId = detail.ColorId,
						SizeId = detail.SizeId
					};

					await _productDetailService.Create(productDetail);
				}

				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}



		// GET: Products/Edit/5
		public async Task<IActionResult> Edit(Guid id)
		{
			if (_productServices.GetAllProducts() == null)
			{
				return NotFound();
			}

			var product = await _productServices.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["BrandId"] = new SelectList(_brandServices.GetAllBrands().Result.Where(x => x.Status == true), "Id", "Name", product.BrandId);
			ViewData["CategoryId"] = new SelectList(_categoryServices.GetAllCategories().Result.Where(x => x.Status == true), "Id", "Name", product.CategoryId);
			ViewData["MaterialId"] = new SelectList(_materialServices.GetAllMaterials().Result.Where(x => x.Status == true), "Id", "Name", product.MaterialId);
			ViewData["SoleId"] = new SelectList(_soleServices.GetAllSoles().Result.Where(x => x.Status == true), "Id", "TypeName", product.SoleId);
			return View(product);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,CategoryId,SoleId,BrandId,MaterialId")] Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}

			if (product.Name != null)
			{
				try
				{
					await _productServices.Update(product);
				}
				catch (Exception ex)
				{
					return Content(ex.Message);
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["BrandId"] = new SelectList(_brandServices.GetAllBrands().Result.Where(x => x.Status == true), "Id", "Name", product.BrandId);
			ViewData["CategoryId"] = new SelectList(_categoryServices.GetAllCategories().Result.Where(x => x.Status == true), "Id", "Name", product.CategoryId);
			ViewData["MaterialId"] = new SelectList(_materialServices.GetAllMaterials().Result.Where(x => x.Status == true), "Id", "Name", product.MaterialId);
			ViewData["SoleId"] = new SelectList(_soleServices.GetAllSoles().Result.Where(x => x.Status == true), "Id", "TypeName", product.SoleId);
			return View(product);
		}

		// GET: Products/Delete/5
		public async Task<IActionResult> Delete(Guid id)
		{
			if (_productServices.GetAllProducts() == null)
			{
				return NotFound();
			}

			var product = await _productServices.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			if (_productServices.GetAllProducts() == null)
			{
				return Problem("Entity set 'Product'  is null.");
			}
			await _productServices.Delete(id);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile imageFile, string colorId)
		{
			if (imageFile != null && imageFile.Length > 0)
			{
				var extension = Path.GetExtension(imageFile.FileName).ToLower();
				var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

				if (!allowedExtensions.Contains(extension))
				{
					return Json(new { success = false, message = "File không hợp lệ" });
				}
				var guidForImg = Guid.NewGuid().ToString();
				// Lưu ảnh vào thư mục hoặc xử lý ảnh
				string dataPath = Path.Combine("/images/", guidForImg + "-" + imageFile.FileName);
				string filePath = Path.Combine("wwwroot/images", guidForImg + "-" + imageFile.FileName);
				var color = _colorServices.GetAllColors().Result;

				if (color == null)
				{
					return Json(new { success = false, message = "Màu không hợp lệ" });
				}

				Image img = new Image()
				{
					Id = Guid.NewGuid(),
					ColorId = Guid.Parse(colorId),
					URL = dataPath,
				};
				try
				{
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await _imageServices.Create(img);
						await imageFile.CopyToAsync(stream);
					}
				}
				catch (Exception ex)
				{
					return Json(new { success = false, message = $"{ex.Message}" });
				}

				return Json(new { success = true });
			}

			return Json(new { success = false, message = "Không có ảnh được tải lên" });
		}

		[HttpPost]
		public async Task<IActionResult> DeleteImage(string id)
		{
			if (id == null) return Json(new { success = false, message = "Không có ảnh được chọn" });
			var img = await _imageServices.GetImageById(Guid.Parse(id));
			var imageName = img.URL.Split('/')[2];

			string filePath = Path.Combine("wwwroot/images", imageName);
			if (System.IO.File.Exists(filePath))
			{
				try
				{
					System.IO.File.Delete(filePath);
					await _imageServices.Delete(Guid.Parse(id));
					return Json(new { success = true });
				}
				catch (Exception ex)
				{
					return Json(new { success = false, message = "Không thể xoá ảnh: " + ex.Message });
				}
			}
			else
			{
				return Json(new { success = false, message = "Không thể xoá ảnh: Không tìm thấy ảnh" });
			}
		}

		[HttpPost]
		public async Task<IActionResult> GetImages(Guid colorId)
		{
			var images = _imageServices.GetAllImages().Result.Where(i => i.ColorId == colorId);

			return Json(new { success = true, images });
		}
	}
}

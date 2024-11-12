using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTO;
using API.Data;
using DataProcessing.Models;
using API.IRepositories;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductRepo _productRepo;

		public ProductsController(IProductRepo productRepo)
		{
			_productRepo = productRepo;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductDTO()
		{
			try
			{
				return await _productRepo.GetAllProduct();
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}

		}

		// GET: api/Products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(Guid id)
		{
			try
			{
				return await _productRepo.GetProductById(id);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		// PUT: api/Products/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(ProductDTO productDTO)
		{
			try
			{
				Product product = new Product()
				{
					Id = productDTO.Id,
					Name = productDTO.Name,
					Description = productDTO.Description,

					SoleId = productDTO.SoleId,
					CategoryId = productDTO.CategoryId,
					BrandId = productDTO.BrandId,
					MaterialId = productDTO.MaterialId,
				};

				await _productRepo.Update(product);
			    await _productRepo.SaveChanges();
			}
			catch (Exception ex)
			{
				return Problem(ex.InnerException.Message);
			}

			return NoContent();
		}

		// POST: api/Products
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDTO)
		{
			try
			{
				Product product = new Product()
				{
					Id = productDTO.Id,
					Name = productDTO.Name,
					Description = productDTO.Description,

					SoleId = productDTO.SoleId,
					CategoryId = productDTO.CategoryId,
					BrandId = productDTO.BrandId,
					MaterialId = productDTO.MaterialId,
				};

				await _productRepo.Create(product);
				await _productRepo.SaveChanges();
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return CreatedAtAction("GetProductDTO", new { id = productDTO.Id }, productDTO);
		}

		// DELETE: api/Products/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductDTO(Guid id)
		{
			try
			{
				await _productRepo.Delete(id);
				await _productRepo.SaveChanges();
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return NoContent();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.IRepositories;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailPromotionsController : ControllerBase
    {
        private readonly IProductDetailPromotionRepos _productDetailPromotionRepository;

        public ProductDetailPromotionsController(IProductDetailPromotionRepos productDetailPromotionRepository)
        {
            _productDetailPromotionRepository = productDetailPromotionRepository;
        }

        // GET: api/ProductDetailPromotions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetailPromotion>>> GetProductDetailPromotions()
        {
            try
            {
                var promotions = await _productDetailPromotionRepository.GetAllAsync();
                return Ok(promotions);
            }
            catch (Exception ex)
            {
                // Trả về thông báo lỗi chung
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        // GET: api/ProductDetailPromotions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailPromotion>> GetProductDetailPromotion(Guid id)
        {
            try
            {
                var productDetailPromotion = await _productDetailPromotionRepository.GetByIdAsync(id);
                if (productDetailPromotion == null)
                {
                    return NotFound();
                }

                return Ok(productDetailPromotion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        // PUT: api/ProductDetailPromotions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductDetailPromotion(Guid id, ProductDetailPromotion productDetailPromotion)
        {
            if (id != productDetailPromotion.Id) // Kiểm tra xem ID có khớp hay không
            {
                return BadRequest(new { message = "ID mismatch." });
            }

            try
            {
                await _productDetailPromotionRepository.UpdateAsync(productDetailPromotion);
                await _productDetailPromotionRepository.SaveChanges();
                return NoContent(); // Trả về 204 No Content nếu thành công
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(new { message = "ProductDetailPromotion not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        // POST: api/ProductDetailPromotions
        [HttpPost]
        public async Task<ActionResult<ProductDetailPromotion>> PostProductDetailPromotion(ProductDetailPromotion productDetailPromotion)
        {
            if (productDetailPromotion == null)
            {
                return BadRequest(new { message = "Invalid input data." });
            }

            try
            {
                // Gọi phương thức AddAsync để thêm đối tượng
                await _productDetailPromotionRepository.AddAsync(productDetailPromotion);
                await _productDetailPromotionRepository.SaveChanges();

                // Trả về thông tin đối tượng vừa tạo
                return CreatedAtAction(nameof(GetProductDetailPromotion), new { id = productDetailPromotion.Id }, productDetailPromotion);
            }
            catch (DbUpdateException dbEx)
            {
                // Xử lý lỗi liên quan đến cơ sở dữ liệu
                return BadRequest(new { message = "Database error: " + dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred: " + ex.Message });
            }
        }

        // DELETE: api/ProductDetailPromotions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetailPromotion(Guid id)
        {
            try
            {
                await _productDetailPromotionRepository.DeleteAsync(id);
                await _productDetailPromotionRepository.SaveChanges();
                return NoContent(); // Trả về 204 No Content nếu xóa thành công
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "ProductDetailPromotion not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}

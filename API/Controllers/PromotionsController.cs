using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using DataProcessing.Models;
using API.IRepositories;
using API.Repositories;
using Data.ViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionRepos _PromorionRepos;

        public PromotionsController(IPromotionRepos promorionRepos)
        {
            _PromorionRepos = promorionRepos;
        }



        // GET: api/Promotions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetPromotions()
        {
            try
            {
                var promotions = await _PromorionRepos.GetAllPromotion();
                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Promotions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> GetPromotion(Guid id)
        {
            try
            {
                var promotion = await _PromorionRepos.GetPromotionById(id);
                if (promotion == null)
                {
                    return NotFound();
                }
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Promotions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromotion( Promotion promotion)
        {
           
            try
            {
                var promotionupdate = await _PromorionRepos.GetPromotionById(promotion.Id);
                promotionupdate.Name = promotion.Name;
                promotionupdate.ProductDetailPromotions = promotion.ProductDetailPromotions;
                promotionupdate.StartDate = promotion.StartDate;
                promotionupdate.EndDate = promotion.EndDate;
                promotionupdate.DiscountValue = promotion.DiscountValue;
                promotionupdate.Status = promotion.Status;

                await _PromorionRepos.Update(promotionupdate);
                await _PromorionRepos.SaveChanges();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Promotions
        [HttpPost]
        public async Task<ActionResult<Promotion>> PostPromotion(Promotion promotion)
        {
            try
            {
               await _PromorionRepos.Create(promotion);
                await _PromorionRepos.SaveChanges();
                return Ok(promotion);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Promotions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            try
            {
                await _PromorionRepos.Delete(id);
                await _PromorionRepos.SaveChanges();
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("ProductDetailsPromotion")]
        public async Task<ActionResult<IEnumerable<ProductDetailsPromotionViewModel>>> GetAllProductDetailsPromotion()
        {
            try
            {
                var ProductDetailsPromotion = await _PromorionRepos.GetAllProductDetailsPromotion();
                return Ok(ProductDetailsPromotion);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

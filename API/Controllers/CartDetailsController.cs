using API.IRepositories;
using DataProcessing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailsController : ControllerBase
    {
        private readonly ICartDetailsRepo _cartRepo;

        public CartDetailsController(ICartDetailsRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }
        [HttpGet("GetAllCartDetails")]
        public async Task<IActionResult> GetAllCartDetails()
        {
            var lstCartDetails = await _cartRepo.GetAllCartDetails();
            return Ok(lstCartDetails);
        }
        [HttpGet("GetCartDetailsByCartId")]
        public async Task<IActionResult> GetCartDetailByCartId(Guid cartId)
        {
            var lstCartDetails = await _cartRepo.GetCartDetailByCartId(cartId);
            if (lstCartDetails == null || lstCartDetails.Count == 0)
            {
                return NotFound();
            }
            return Ok(lstCartDetails);
        }
        [HttpGet("GetCartDetailsById")]
        public async Task<IActionResult> GetCartDetailById(Guid id)
        {
            var cartDetails = await _cartRepo.GetCartDetailById(id);
            if (cartDetails == null)
            {
                return NotFound();
            }
            return Ok(cartDetails);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CartDetail cartDetail)
        {
            await _cartRepo.Create(cartDetail); // Thêm await cho đúng
            return Ok();
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(CartDetail cartDetails, Guid id)
        {
            await _cartRepo.Update(cartDetails, id);
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cartRepo.Delete(id);
            return Ok();
        }
    }
}

using API.IRepositories;
using API.Repositories;
using DataProcessing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _repo;

        public CartController(ICartRepo repo)
        {
            _repo = repo;
        }     
        [HttpGet("GetCartById")]
        public async Task<IActionResult> GetCartById(Guid id)
        {
            var cart = await _repo.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
        [HttpGet("GetCartByUserId")]
        public async Task<IActionResult> GetCartByUserId(Guid userId)
        {
            var cart = await _repo.GetCartByUserId(userId);
            if(cart == null)
            {
                return Ok(null);
            }    
            return Ok(cart);
        }
        [HttpPost("CreateCart")]
        public async Task<IActionResult> Create(Cart cart)
        {
            await _repo.Create(cart);
            return Ok(cart);
        }
        [HttpPut("UpdateCart")]
        public async Task<IActionResult> Update(Guid id , Cart cart)
        { 
            await _repo.Update(id, cart);
            return Ok();
        }
    }
}

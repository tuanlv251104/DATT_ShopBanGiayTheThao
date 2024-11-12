using API.IRepositories;
using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderAdressesController : ControllerBase
	{
		private readonly IOrderAddressRepo _orderAddressRepo;
        public OrderAdressesController(IOrderAddressRepo orderAddressRepo)
        {
            _orderAddressRepo = orderAddressRepo;
        }
        // GET: api/<OrderAdressesController>
        [HttpGet]
		public async Task<IEnumerable<OrderAdress>?> Get()
		{
			return await _orderAddressRepo.GetAllOrderAdresses();
		}

		// GET api/<OrderAdressesController>/5
		[HttpGet("OrderId/{id}")]
		public async Task<ActionResult<OrderAdress>> Get(Guid id)
		{
			return await _orderAddressRepo.GetOrderAdressByOrderId(id);
		}

		// POST api/<OrderAdressesController>
		[HttpPost]
		public async Task<ActionResult<OrderAdress>> Post(OrderAdress orderAddress)
		{
			try
			{
				await _orderAddressRepo.Create(orderAddress);
				await _orderAddressRepo.SaveChanges();
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}
			return CreatedAtAction("Get", new { orderAddress.OrderId }, orderAddress);
		}

		// PUT api/<OrderAdressesController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<OrderAdress>> Put(Guid id, OrderAdress orderAdress)
		{
			try
			{
				await _orderAddressRepo.Update(orderAdress);
				await _orderAddressRepo.SaveChanges();
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}
			return CreatedAtAction("Get", new { id }, orderAdress);
		}

		// DELETE api/<OrderAdressesController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await _orderAddressRepo.Delete(id);
				await _orderAddressRepo.SaveChanges();
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return Ok();
		}
	}
}

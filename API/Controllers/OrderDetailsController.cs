using API.IRepositories;
using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderDetailsController : ControllerBase
	{
		private readonly IOrderDetailRepo _orderDetailRepo;
        public OrderDetailsController(IOrderDetailRepo orderDetailRepo)
        {
            _orderDetailRepo = orderDetailRepo;
        }
        // GET: api/<OrderDetailsController>
        [HttpGet]
		public async Task<ActionResult<IEnumerable<OrderDetail>>> Get()
		{
			return await _orderDetailRepo.GetAllOrderDetails();
		}

		// GET api/<OrderDetailsController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderDetail>> Get(Guid id)
		{
			return await _orderDetailRepo.GetOrderDetailById(id);
		}

		[HttpGet("OrderId/{id}")]
		public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetailsByOrderId(Guid id)
		{
			return await _orderDetailRepo.GetOrderDetailsByOrderId(id);
		}

		// POST api/<OrderDetailsController>
		[HttpPost]
		public async Task<ActionResult<OrderDetail>> Post(OrderDetail orderDetail)
		{
			try
			{
				await _orderDetailRepo.Create(orderDetail);
				await _orderDetailRepo.SaveChanges();
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return CreatedAtAction("Get", new { id = orderDetail.Id }, orderDetail);
		}

		// PUT api/<OrderDetailsController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(Guid id, OrderDetail orderDetail)
		{
			try
			{
				await _orderDetailRepo.Update(orderDetail);
				await _orderDetailRepo.SaveChanges();
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return CreatedAtAction("Get", new { id = orderDetail.Id }, orderDetail);
		}

		// DELETE api/<OrderDetailsController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await _orderDetailRepo.Delete(id);
				await _orderDetailRepo.SaveChanges();
			}catch (Exception ex)
			{
				Problem(ex.Message);
			}

			return Content("Success");
		}
	}
}

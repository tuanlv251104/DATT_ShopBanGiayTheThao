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
using API.DTO;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderRepo _orderRepo;

		public OrdersController(IOrderRepo orderRepo)
		{
			_orderRepo = orderRepo;
		}

		// GET: api/Orders
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
		{
			return await _orderRepo.GetAllOrders();
		}

		[HttpGet("UserId/{id}")]
		public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(string id)
		{
			return await _orderRepo.GetAllOrderByUser(id);
		}

		// GET: api/Orders/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrder(Guid id)
		{
			if (await _orderRepo.GetAllOrders() == null)
			{
				return NotFound();
			}
			var order = await _orderRepo.GetOrderById(id);

			if (order == null)
			{
				return NotFound();
			}

			return order;
		}

		// PUT: api/Orders/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutOrder(Guid id, Order order)
		{
			try
			{
				await _orderRepo.Update(order);
				await _orderRepo.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (_orderRepo.GetOrderById(id) == null)
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetOrder", new { id = order.Id }, order);
		}

		// POST: api/Orders
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Order>> PostOrder(Order order)
		{
			try
			{
				await _orderRepo.Create(order);
				await _orderRepo.SaveChanges();
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return CreatedAtAction("GetOrder", new { id = order.Id }, order);
		}

		// DELETE: api/Orders/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(Guid id)
		{
			try
			{
				await _orderRepo.Delete(id);
				await _orderRepo.SaveChanges();
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			return Content("Success");
		}
	}
}

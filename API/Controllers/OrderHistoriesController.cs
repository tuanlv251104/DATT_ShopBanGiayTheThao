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
    public class OrderHistoriesController : ControllerBase
    {
        private readonly IOrderHistoryRepo _orderHistoryRepo;

        public OrderHistoriesController(IOrderHistoryRepo context)
        {
            _orderHistoryRepo = context;
        }

        // GET: api/OrderHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderHistory>>> GetOrderHistories()
        {
            return await _orderHistoryRepo.GetAllHistories();
        }

		[HttpGet("OrderId/{id}")]
		public async Task<ActionResult<IEnumerable<OrderHistory>>> GetOrderHistoriesByOrderId(Guid id)
		{
			return await _orderHistoryRepo.GetAllHistoriesByOrderId(id);
		}

		// GET: api/OrderHistories/5
		[HttpGet("{id}")]
        public async Task<ActionResult<OrderHistory>> GetOrderHistory(Guid id)
        {
          if (_orderHistoryRepo.GetAllHistories() == null)
          {
              return NotFound();
          }
            var orderHistory = await _orderHistoryRepo.GetHistoryById(id);

            if (orderHistory == null)
            {
                return NotFound();
            }

            return orderHistory;
        }

        // PUT: api/OrderHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderHistory(Guid id, OrderHistory orderHistory)
        {
            if (id != orderHistory.Id)
            {
                return BadRequest();
            }

            OrderHistory data = new OrderHistory()
            {
                Id = orderHistory.Id,
                StatusType = orderHistory.StatusType,
                TimeStamp = orderHistory.TimeStamp,
                Note = orderHistory.Note,
                UpdatedByUserId = orderHistory.UpdatedByUserId,
                OrderId = orderHistory.OrderId,
            };
            await _orderHistoryRepo.Update(data);

            try
            {
				await _orderHistoryRepo.SaveChanges();
			}
            catch (DbUpdateConcurrencyException)
            {
                if (await _orderHistoryRepo.GetHistoryById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

			return CreatedAtAction("GetOrderHistory", new { id = orderHistory.Id }, orderHistory);
		}

        // POST: api/OrderHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderHistory>> PostOrderHistory(OrderHistory orderHistory)
        {
            try
            {
				OrderHistory data = new OrderHistory()
				{
					Id = orderHistory.Id,
					StatusType = orderHistory.StatusType,
					TimeStamp = orderHistory.TimeStamp,
					Note = orderHistory.Note,
					UpdatedByUserId = orderHistory.UpdatedByUserId,
					OrderId = orderHistory.OrderId,
				};
				await _orderHistoryRepo.Create(data);
                await _orderHistoryRepo.SaveChanges();
            }catch(Exception ex)
            {
                Problem(ex.InnerException.Message);
            }

            return CreatedAtAction("GetOrderHistory", new { id = orderHistory.Id }, orderHistory);
        }

        // DELETE: api/OrderHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderHistory(Guid id)
        {
            try
            {
                await _orderHistoryRepo.Delete(id);
                await _orderHistoryRepo.SaveChanges();
            }catch(Exception ex)
            {
                Problem(ex.InnerException.Message);
            }

            return NoContent();
        }
    }
}

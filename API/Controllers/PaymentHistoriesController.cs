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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentHistoriesController : ControllerBase
    {
        private readonly IPaymentHistoryRepo _historyRepo;

        public PaymentHistoriesController(IPaymentHistoryRepo historyRepo)
        {
            _historyRepo = historyRepo;
        }

        // GET: api/PaymentHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentHistory>>> GetPaymentHistories()
        {
          if (await _historyRepo.GetAllPaymentHistories() == null)
          {
              return NotFound();
          }
            return await _historyRepo.GetAllPaymentHistories();
        }

        [HttpGet("OrderId/{id}")]
        public async Task<ActionResult<IEnumerable<PaymentHistory>>> GetPaymentHistoriesByOrderId(Guid id)
        {
            if(await _historyRepo.GetPaymentHistoriesByOrderId(id) is null) return NotFound();
            return await _historyRepo.GetPaymentHistoriesByOrderId(id);
        }

        // GET: api/PaymentHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentHistory>> GetPaymentHistory(Guid id)
        {
          if (await _historyRepo.GetAllPaymentHistories() == null)
          {
              return NotFound();
          }
            var paymentHistory = await _historyRepo.GetHistoryById(id);

            if (paymentHistory == null)
            {
                return NotFound();
            }

            return paymentHistory;
        }

        // PUT: api/PaymentHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentHistory(Guid id, PaymentHistory paymentHistory)
        {
            if (id != paymentHistory.Id)
            {
                return BadRequest();
            }

            await _historyRepo.Update(paymentHistory);

            try
            {
                await _historyRepo.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _historyRepo.GetHistoryById(id) is null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

			return CreatedAtAction("GetPaymentHistory", new { id = paymentHistory.Id }, paymentHistory);
		}

        // POST: api/PaymentHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentHistory>> PostPaymentHistory(PaymentHistory paymentHistory)
        {
            try
            {
                await _historyRepo.Create(paymentHistory);
                await _historyRepo.SaveChanges();
            }catch (Exception ex)
            {
                Problem(ex.Message);
            }
            return CreatedAtAction("GetPaymentHistory", new { id = paymentHistory.Id }, paymentHistory);
        }

        // DELETE: api/PaymentHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentHistory(Guid id)
        {
            try
            {
                await _historyRepo.Delete(id);
                await _historyRepo.SaveChanges();
            }catch (Exception ex)
            {
                Problem(ex.Message);
            }

            return NoContent();
        }
    }
}

using DataProcessing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using View.IServices;
using View.ViewModel;

namespace View.Controllers
{
	public class CounterSalesController : Controller
	{
		private readonly IOrderServices _orderServices;
		private readonly IShippingUnitServices _shippingUnitServices;
		private readonly IVoucherServices _voucherServices;
		private readonly IAccountService _accountService;
		public CounterSalesController(IOrderServices orderServices, IShippingUnitServices shippingUnitServices, IVoucherServices voucherServices, IAccountService accountService)
        {
            _orderServices = orderServices;
			_shippingUnitServices = shippingUnitServices;
			_voucherServices = voucherServices;
			_accountService = accountService;
        }
        // GET: CounterSalesController
        public ActionResult Index()
		{
			var token = HttpContext.Session.GetString("AuthToken");
			var userId = "";
			if (!string.IsNullOrEmpty(token))
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var jwtToken = tokenHandler.ReadJwtToken(token);

				var claims = jwtToken.Claims.ToList();
				userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			}

			var orders = new CounterSalesVM()
			{
				Products = _orderServices.GetProductDetails().Result,
				Customers = _accountService.GetAllCustomer().Result,
				Orders = _orderServices.GetAllOrdersByStatus().Result.Where(o => o.WhoCreateThis == Guid.Parse(userId) && o.Status == "Tạo đơn hàng"),
			};
			ViewData["ShippingUnitID"] = new SelectList(_shippingUnitServices.GetAllShippingUnit().Result, "ShippingUnitID", "Address");
			ViewData["VoucherId"] = new SelectList(_voucherServices.GetAllVouchers().Result, "Id", "Condittion");
			return View(orders);
		}

		// GET: CounterSalesController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: CounterSalesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CounterSalesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,CreatedDate,TotalPrice,PaymentMethod,Status,AddressId,UserId,VoucherId,ShippingUnitID")] Order order)
		{
			try
			{
				var token = HttpContext.Session.GetString("AuthToken");
				var userId = "";
				if (!string.IsNullOrEmpty(token))
				{
					var tokenHandler = new JwtSecurityTokenHandler();
					var jwtToken = tokenHandler.ReadJwtToken(token);

					var claims = jwtToken.Claims.ToList();
					userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
				}

				if (ModelState.IsValid)
				{
					order.Id = Guid.NewGuid();
					await _orderServices.Create(Guid.Parse(userId), order);
				}
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CounterSalesController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CounterSalesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CounterSalesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CounterSalesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}

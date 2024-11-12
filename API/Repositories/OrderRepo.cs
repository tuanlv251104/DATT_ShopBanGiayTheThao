using API.Data;
using API.IRepositories;
using DataProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class OrderRepo : IOrderRepo
	{
		private readonly ApplicationDbContext _context;
        public OrderRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Order order)
		{
			if (await GetOrderById(order.Id) != null) throw new DuplicateWaitObjectException("This order is existed!");
			await _context.Orders.AddAsync(order);
		}

		public async Task Delete(Guid id)
		{
			var data = GetOrderById(id).Result;
			if (data == null) throw new KeyNotFoundException("This order is not existed!");
			_context.Orders.Remove(data);
		}

		public async Task<List<Order>?> GetAllOrderByUser(string userId)
		{
			var data = await _context.Orders.Where(o => o.UserId == userId)
				.Include(o => o.Voucher)
				.Include(o => o.ShippingUnit)
				.Include(o => o.OrderAddress)
				.ToListAsync();
			return data;
		}

		public async Task<List<Order>?> GetAllOrders()
		{
			return await _context.Orders
				.Include(o => o.ShippingUnit)
				.Include(o => o.Voucher)
				.Include(o => o.OrderAddress)
				.ToListAsync();
		}

		public async Task<Order?> GetOrderById(Guid id)
		{
			return await _context.Orders.Where(o => o.Id == id)
				.Include(o => o.ShippingUnit)
				.Include(o => o.Voucher)
				.Include(o => o.OrderAddress)
				.FirstOrDefaultAsync();
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(Order order)
		{
			var data = await GetOrderById(order.Id);
			if (data == null) throw new KeyNotFoundException("This order is not existed!");
			_context.Entry(order).State = EntityState.Modified;
		}
	}

	public class OrderHistoryRepo : IOrderHistoryRepo
	{
		private readonly ApplicationDbContext _context;
		public OrderHistoryRepo(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task Create(OrderHistory OrderHistory)
		{
			if (await GetHistoryById(OrderHistory.Id) != null) throw new DuplicateWaitObjectException("This OrderHistory is existed!");
			await _context.OrderHistories.AddAsync(OrderHistory);
		}

		public async Task Delete(Guid id)
		{
			var data = GetHistoryById(id).Result;
			if (data != null) throw new KeyNotFoundException("This OrderHistory is not existed!");
			_context.OrderHistories.Remove(data);
		}

		public async Task<List<OrderHistory>?> GetAllHistoriesByOrderId(Guid id)
		{
			var data = await _context.OrderHistories.Where(o => o.OrderId == id)
				.Include(o => o.Order)
				.ToListAsync();
			return data;
		}

		public async Task<List<OrderHistory>?> GetAllHistories()
		{
			return await _context.OrderHistories
				.Include(o => o.Order)
				.ToListAsync();
		}

		public async Task<OrderHistory?> GetHistoryById(Guid id)
		{
			return await _context.OrderHistories.Where(o => o.Id == id)
				.Include(o => o.Order)
				.FirstOrDefaultAsync();
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(OrderHistory OrderHistory)
		{
			var data = await GetHistoryById(OrderHistory.Id);
			if (data == null) throw new KeyNotFoundException("This OrderHistory is not existed!");
			_context.Entry(OrderHistory).State = EntityState.Modified;
		}
	}

	public class PaymentHistoryRepo : IPaymentHistoryRepo
	{
		private readonly ApplicationDbContext _context;
        public PaymentHistoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(PaymentHistory payment)
		{
			if (await GetHistoryById(payment.Id) is not null) throw new DuplicateWaitObjectException("This record is existed!");
			await _context.PaymentHistories.AddAsync(payment);
		}

		public async Task Delete(Guid id)
		{
			var data = await GetHistoryById(id);
			if (data is null) throw new KeyNotFoundException("Not found this record");
			_context.PaymentHistories.Remove(data);
		}

		public async Task<List<PaymentHistory>> GetAllPaymentHistories()
		{
			return await _context.PaymentHistories
				.Include(ph => ph.Order)
				.ToListAsync();
		}

		public async Task<PaymentHistory?> GetHistoryById(Guid id)
		{
			return await _context.PaymentHistories
				.Include(ph => ph.Order)
				.FirstOrDefaultAsync(ph => ph.Id == id);
		}

		public async Task<List<PaymentHistory>> GetPaymentHistoriesByOrderId(Guid id)
		{
			return await _context.PaymentHistories
				.Where(ph => ph.OrderId == id)
				.Include(ph => ph.Order)
				.ToListAsync();
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(PaymentHistory payment)
		{
			if (await GetHistoryById(payment.Id) is null) throw new KeyNotFoundException("Not found this record");
			_context.Entry(payment).State = EntityState.Modified;
		}
	}

	public class OrderDetailRepo : IOrderDetailRepo
	{
		private readonly ApplicationDbContext _context;
		public OrderDetailRepo(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Create(OrderDetail orderDetail)
		{
			if (await GetOrderDetailById(orderDetail.Id) is not null) throw new DuplicateWaitObjectException("This orderDetail is existed!");
			await _context.OrderDetails.AddAsync(orderDetail);
		}

		public async Task Delete(Guid id)
		{
			var data = await GetOrderDetailById(id);
			if (data is null) throw new KeyNotFoundException("Not found this orderDetail");
			_context.OrderDetails.Remove(data);
		}

		public async Task<List<OrderDetail>?> GetAllOrderDetails()
		{
			return await _context.OrderDetails
				.Include(od => od.Order)
				.Include(od => od.ProductDetail)
					.ThenInclude(od => od.Product)
				.ToListAsync();
		}

		public async Task<OrderDetail?> GetOrderDetailById(Guid id)
		{
			return await _context.OrderDetails
				.Where(od => od.Id == id)
				.Include(od => od.Order)
				.Include(od => od.ProductDetail)
					.ThenInclude(od => od.Product)
				.FirstOrDefaultAsync();
		}

		public async Task<List<OrderDetail>?> GetOrderDetailsByOrderId(Guid id)
		{
			return await _context.OrderDetails
				.Where(od => od.OrderId == id)
				.Include(od => od.Order)
				.Include(od => od.ProductDetail)
					.ThenInclude(od => od.Product)
				.ToListAsync();
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(OrderDetail orderDetail)
		{
			var data = await GetOrderDetailById(orderDetail.Id);
			if (data is null) throw new KeyNotFoundException("Not found this orderDetail");
			_context.Entry(orderDetail).State = EntityState.Modified;
		}
	}

	public class OrderAddressRepo : IOrderAddressRepo
	{
		private readonly ApplicationDbContext _context;
        public OrderAddressRepo(ApplicationDbContext context)
        {
			_context = context;
        }
        public async Task Create(OrderAdress orderAdress)
		{
			if (await _context.OrderAdresses.FindAsync(orderAdress.Id) != null) throw new DuplicateWaitObjectException("This Id already existed!");
			await _context.OrderAdresses.AddAsync(orderAdress);
		}

		public async Task Delete(Guid id)
		{
			var data = _context.OrderAdresses.FindAsync(id).Result;
			if (data == null) throw new KeyNotFoundException("This Id no found!");
			_context.OrderAdresses.Remove(data);

		}

		public async Task<List<OrderAdress>?> GetAllOrderAdresses()
		{
			return await _context.OrderAdresses
				.ToListAsync();
		}

		public async Task<OrderAdress?> GetOrderAdressByOrderId(Guid id)
		{
			return _context.OrderAdresses
				.Where(oa => oa.OrderId == id)
				.FirstOrDefaultAsync().Result;
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(OrderAdress orderAdress)
		{
			if (await _context.OrderAdresses.FindAsync(orderAdress.Id) == null) throw new KeyNotFoundException("This Id no found!");
			_context.Entry(orderAdress).State = EntityState.Modified;
		}
	}
}

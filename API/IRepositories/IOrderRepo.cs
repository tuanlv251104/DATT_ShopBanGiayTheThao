using DataProcessing.Models;

namespace API.IRepositories
{
	public interface IOrderRepo
	{
		Task<List<Order>> GetAllOrders();
		Task<Order> GetOrderById(Guid id);
		Task<List<Order>> GetAllOrderByUser(string userId);
		Task Create(Order order);
		Task Update(Order order);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface IOrderHistoryRepo
	{
		Task<List<OrderHistory>> GetAllHistories();
		Task<OrderHistory> GetHistoryById(Guid id);
		Task<List<OrderHistory>> GetAllHistoriesByOrderId(Guid id);
		Task Create(OrderHistory orderHistory);
		Task Update(OrderHistory orderHistory);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface IPaymentHistoryRepo
	{
		Task<List<PaymentHistory>> GetAllPaymentHistories();
		Task<List<PaymentHistory>> GetPaymentHistoriesByOrderId(Guid id);
		Task<PaymentHistory> GetHistoryById(Guid id);
		Task Create(PaymentHistory payment);
		Task Update(PaymentHistory payment);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface IOrderDetailRepo
	{
		Task<List<OrderDetail>?> GetAllOrderDetails();
		Task<List<OrderDetail>?> GetOrderDetailsByOrderId(Guid id);
		Task<OrderDetail?> GetOrderDetailById(Guid id);
		Task Create(OrderDetail orderDetail);
		Task Update(OrderDetail orderDetail);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface IOrderAddressRepo
	{
		Task<List<OrderAdress>?> GetAllOrderAdresses();
		Task<OrderAdress> GetOrderAdressByOrderId(Guid id);
		Task Create(OrderAdress orderAdress);
		Task Update(OrderAdress orderAdress);
		Task Delete(Guid id);
		Task SaveChanges();
	}
}

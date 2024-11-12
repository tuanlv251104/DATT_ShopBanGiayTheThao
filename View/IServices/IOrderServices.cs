using DataProcessing.Models;

namespace View.IServices
{
	public interface IOrderServices
	{
		Task<IEnumerable<Order>> GetAllOrdersByStatus();
		Task<IEnumerable<Order>> GetAllOrderByUserId(Guid id);
		Task<Order> GetOrderById(Guid id);
		Task Create(Guid UserIdCreateThis, Order order);
		Task Update(Order order);
		Task Delete(Guid id);
		Task<IEnumerable<PaymentHistory>> GetPaymentHistoriesByOrderId(Guid id);
		Task AddPayment(PaymentHistory payment);
		Task ChangeStatus(Guid UserIdCreateThis, Guid OrderId); // OrderHistory
		Task BackStatus(Guid UserIdCreateThis, Guid OrderId); // OrderHistory
		Task CancelOrder(Guid UserIdCreateThis, Guid OrderId); // OrderHistory
		Task<IEnumerable<OrderHistory>> GetOrderHistoriesByOrderId(Guid id);
		Task<IEnumerable<OrderDetail>> GetAllOrderDetailsByOrderId(Guid id);
		Task AddToOrder(OrderDetail order);
		Task DeleteFromOrder(Guid id);
		Task ChangeStock(int stock, Guid orderDetailId);
		Task<IEnumerable<ProductDetail>> GetProductDetails();

		Task<OrderAdress?> GetOrderAddressByOrderId(Guid id);
		Task ChangeOrderAddress(OrderAdress orderAdress);
		Task AddOrderAddress(OrderAdress orderAdress);
	}
}

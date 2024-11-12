using DataProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewModel
{
	public class OrderVM
	{
		public Order Orders { get; set; }
		public IEnumerable<OrderHistory> OrderHistories { get; set; }
		public IEnumerable<PaymentHistory> PaymentHistories { get; set; }
		public IEnumerable<OrderDetail> OrderDetails { get; set; }
		public int quantity { get; set; }
		public IEnumerable<ProductDetail> ProductDetails { get; set; }
		public OrderAdress? OrderAdress { get; set; }
	}
}

using DataProcessing.Models;

namespace View.ViewModel
{
    public class CounterSalesVM
    {
        public IEnumerable<ProductDetail>? Products { get; set; }
        public IEnumerable<ApplicationUser>? Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}

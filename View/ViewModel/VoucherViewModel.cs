using DataProcessing.Models;
using System.Security.Principal;

namespace View.ViewModel
{
    public class VoucherViewModel
    {
        public Voucher Voucher { get; set; } // Đối tượng Voucher
        public List<ApplicationUser> Accounts { get; set; } // Danh sách các ApplicationUser để chọn
        public int CurrentPage { get; set; } // Trang hiện tại (cho phân trang)
        public int TotalPages { get; set; } // Tổng số trang (cho phân trang)
    }

}

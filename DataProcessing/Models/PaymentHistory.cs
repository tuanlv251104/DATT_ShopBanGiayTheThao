using DataProcessing.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
	public class PaymentHistory
	{
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PaymentType { get; set; } = "Thanh toán";// Loại giao dịch
        public string? PaymentCode { get; set; } // Mã giao dịch
        [Required]
        public decimal PaymentAmount { get; set;} // Tổng tiền của đơn hàng
        public string? Note { get; set; }
        [Required]
        public string PaymentMethod { get; set; } = "Tiền mặt";
        [Required]
        public Guid ConfirmerId { get; set; } // Nhân viên xác nhận
        public bool Status { get; set; } // true = Thành công - false = Thất bại
        [Required]
        public Guid OrderId {  get; set; } // khoá phụ nối tới hoá đơn
        [JsonIgnore]
        public virtual Order? Order { get; set; }
    }
}

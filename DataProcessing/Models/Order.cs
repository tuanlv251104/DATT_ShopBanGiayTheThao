using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        public string? Status { get; set; } = "Chờ xác nhận";
        public virtual OrderAdress? OrderAddress { get; set; }

        public string? UserId { get; set; } = "Khách lẻ";
        public Guid? WhoCreateThis { get; set; }
        public Guid? VoucherId { get; set; }
        [JsonIgnore]
        public virtual Voucher? Voucher { get; set; }
        public Guid? ShippingUnitID { get; set; }
        public virtual ShippingUnit? ShippingUnit { get; set; }
        [JsonIgnore]
        public ICollection<PaymentHistory>? paymentHistories { get; set; }
    }
}

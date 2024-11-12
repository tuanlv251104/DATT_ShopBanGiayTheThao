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
	public class Address
	{
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên người nhận không được để trống.")]
        public string RecipientName { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Địa chỉ chi tiết không được để trống.")]
        public string AddressDetail { get; set; }

        [Required(ErrorMessage = "Thành phố không được để trống.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Quận/Huyện không được để trống.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Xã/Phường không được để trống.")]
        public string Commune { get; set; }

        [Required(ErrorMessage = "UserId không được để trống.")]
        public Guid AccountId { get; set; }

        [ForeignKey("AccountId")]
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }

        public bool IsDeleted { get; set; } = false; // Đánh dấu xóa mềm
    }
}

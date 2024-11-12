using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataProcessing.Models
{
    public class Voucher : IValidatableObject
    {
        // Khóa chính duy nhất
        public Guid Id { get; set; }

        // Mã phiếu giảm giá bắt buộc
        [Required(ErrorMessage = "Mã phiếu giảm giá là bắt buộc")]
        public string VoucherCode { get; set; }

        // Tên phiếu giảm giá bắt buộc, không quá 100 ký tự
        [Required(ErrorMessage = "Tên phiếu giảm giá là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        public string Name { get; set; }

        // Loại giảm giá (Phần trăm hoặc Tiền)
        [Required(ErrorMessage = "Loại giảm giá là bắt buộc")]
        public string DiscountType { get; set; } // "Percent" hoặc "Amount"

        // Giá trị giảm giá phần trăm (giới hạn từ 0% đến 100%) nếu loại là Phần trăm
        [Range(0, 100, ErrorMessage = "Giá trị giảm phải từ 0 đến 100.")]
        public decimal? DiscountPercent { get; set; }

        // Giá trị giảm giá trực tiếp nếu loại là Tiền
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị giảm phải là một giá trị dương.")]
        public decimal? DiscountAmount { get; set; }

        // Giá trị giảm giá tối đa (áp dụng cho loại phần trăm)
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị tối đa phải là một giá trị dương.")]
        public decimal MaxDiscountValue { get; set; }

        // Số lượng tồn kho phải là số nguyên dương hoặc bằng 0
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là một số nguyên không âm.")]
        public int Stock { get; set; }

        // Điều kiện áp dụng voucher (không bắt buộc)
        public string Condition { get; set; }

        // Ngày bắt đầu (phải là hôm nay hoặc tương lai)
        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        [DataType(DataType.DateTime)]
        [CustomStartDateValidation(ErrorMessage = "Ngày bắt đầu không thể là quá khứ.")]
        public DateTime StartDate { get; set; }

        // Ngày kết thúc (phải sau ngày bắt đầu)
        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        [DataType(DataType.DateTime)]
        [CustomEndDateValidation("StartDate", ErrorMessage = "Ngày kết thúc phải sau ngày bắt đầu.")]
        public DateTime EndDate { get; set; }

        // Kiểu phiếu giảm giá (Công khai / Cá nhân)
        public string Type { get; set; }

        // Trạng thái của voucher (true/false)
        [Required(ErrorMessage = "Kiểu phiếu giảm giá là bắt buộc")]
        public bool Status { get; set; }

        // ID của tài khoản (không bắt buộc)
        public Guid? AccountId { get; set; }

        // Phương thức Validate để đảm bảo chỉ một trong hai giá trị giảm giá được điền
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DiscountType == "Percent" && (DiscountPercent == null || DiscountPercent <= 0))
            {
                yield return new ValidationResult("Vui lòng nhập giá trị phần trăm giảm giá hợp lệ.", new[] { nameof(DiscountPercent) });
            }
            else if (DiscountType == "Amount" && (DiscountAmount == null || DiscountAmount <= 0))
            {
                yield return new ValidationResult("Vui lòng nhập giá trị giảm giá hợp lệ.", new[] { nameof(DiscountAmount) });
            }

            if (DiscountType == "Percent" && DiscountAmount != null)
            {
                yield return new ValidationResult("Không thể điền đồng thời cả giá trị giảm phần trăm và tiền mặt.", new[] { nameof(DiscountAmount) });
            }
            if (DiscountType == "Amount" && DiscountPercent != null)
            {
                yield return new ValidationResult("Không thể điền đồng thời cả giá trị giảm phần trăm và tiền mặt.", new[] { nameof(DiscountPercent) });
            }
        }
    }
}

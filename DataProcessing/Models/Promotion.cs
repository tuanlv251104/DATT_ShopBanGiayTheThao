using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class CustomStartDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime startDate = (DateTime)value;
            if (startDate < DateTime.Today)
            {
                return new ValidationResult(ErrorMessage ?? "Start date must be today or later.");
            }
            return ValidationResult.Success;
        }
    }
    public class CustomEndDateValidationAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public CustomEndDateValidationAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime endDate = (DateTime)value;
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            if (startDateProperty == null)
            {
                return new ValidationResult($"Unknown property: {_startDatePropertyName}");
            }

            DateTime startDate = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);

            if (endDate <= startDate)
            {
                return new ValidationResult(ErrorMessage ?? "End date must be after the start date.");
            }

            return ValidationResult.Success;
        }
    }
    public class Promotion
    {
        // Khóa chính duy nhất
        public Guid Id { get; set; }

        // Tên chương trình khuyến mãi bắt buộc, không quá 100 ký tự
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        // Giá trị giảm giá phải lớn hơn 0 và không âm
        [Range(0.01, double.MaxValue, ErrorMessage = "Discount value must be greater than 0")]
        public decimal DiscountValue { get; set; }

        // Ngày bắt đầu khuyến mãi, phải là ngày hôm nay hoặc trong tương lai
        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [CustomStartDateValidation(ErrorMessage = "Start date cannot be in the past.")]
        public DateTime StartDate { get; set; }

        // Ngày kết thúc khuyến mãi, phải sau ngày bắt đầu
        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [CustomEndDateValidation("StartDate", ErrorMessage = "End date must be after the start date.")]
        public DateTime EndDate { get; set; }

        // Trạng thái của chương trình khuyến mãi (true/false)
        public bool Status { get; set; }
        [JsonIgnore]
        public ICollection<ProductDetailPromotion>? ProductDetailPromotions { get; set; }
    }
}

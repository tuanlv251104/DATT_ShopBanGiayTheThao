using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class SignUpModel
    {
        [Required(ErrorMessage ="Tên là bắt buộc")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "SDT là bắt buộc")]
        public string PhoneNumber { get; set; } = null!;
        public string? CIC { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password không đúng định dạng")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "mật khẩu nhập lại không trùng khớp")]
        public string ConfirmPassword { get; set; } = null!;
    }
}

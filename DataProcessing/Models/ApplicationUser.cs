using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string? Name { get; set; } 
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^(03|05|07|08|09)\d{8}$", ErrorMessage = "Invalid Vietnamese phone number format")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [RegularExpression(@"^\d{12}$", ErrorMessage = "CIC must be exactly 12 digits")]
        [Display(Name = "CIC")]
        public string? CIC { get; set; }    
        public string? ImageURL { get; set; }
        public bool IsSubscribedToNews { get; set; } = false; // Mặc định là không đăng ký
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual Cart Cart { get; set; }
    }
}

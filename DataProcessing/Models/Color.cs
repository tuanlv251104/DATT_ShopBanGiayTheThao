using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class Color
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "HEX color is required")]
        [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "Invalid HEX color format")]
        [Display(Name = "HEX Color")]
        public string HEX { get; set; }
        public bool Status { get; set; }

    }
}

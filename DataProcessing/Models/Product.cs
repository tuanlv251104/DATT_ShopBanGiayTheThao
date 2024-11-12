using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }
        public string? Description { get; set; }


        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid SoleId { get; set; }
        public virtual Sole Sole { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public Guid MaterialId { get; set; }
        public virtual Material Material { get; set; }
    }
}

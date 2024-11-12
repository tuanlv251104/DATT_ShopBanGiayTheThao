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
    public class ProductDetail
    {
        public string? Id { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public float Weight { get; set; }


        //public Guid? PromotionId { get; set; }
        //public virtual Promotion Promotion { get; set; }
        public Guid ProductId { get; set; }
        public Guid ColorId { get; set; }
        public Guid SizeId { get; set; }


        //[JsonIgnore]
        public virtual Product? Product { get; set; }
        //[JsonIgnore]
        public virtual Color? Color { get; set; }
        //[JsonIgnore]
        public virtual Size? Size { get; set; }
        //[JsonIgnore]
        public ICollection<ProductDetailPromotion>? ProductDetailPromotions { get; set; }

    }
}

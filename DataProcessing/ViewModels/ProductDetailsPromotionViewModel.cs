using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class ProductDetailsPromotionViewModel
    {
        public Guid promotionId { get; set; }
        public string? ProductDetailsID { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public string? MaterialName { get; set; }
        public int SizeValue { get; set; }
        public string? ColorName { get; set; }
        public string? SoleName { get; set; }
        public decimal PriceProductDetail { get; set; }
        public decimal PricePromotion { get; set; }

    }
}

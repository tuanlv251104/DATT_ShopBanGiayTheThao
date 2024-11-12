using DataProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProductDetailPromotion
    {

        public Guid Id { get; set; }
       
        public string ProductDetailId { get; set; }
        
        public Guid PromotionId { get; set; }

        public decimal PriceUpdate { get; set; }
        [JsonIgnore]
        public virtual ProductDetail? ProductDetail { get; set; }
        [JsonIgnore]
        public virtual Promotion? Promotion { get; set; }
    }
}

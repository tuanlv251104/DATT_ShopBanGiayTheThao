    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class CartDetail
    {
        public Guid Id { get; set; }
        public decimal? TotalPrice { get; set; }
        public int Quanlity { get; set; }

        public Guid CartId { get; set; }
        [JsonIgnore]
        public virtual Cart? Cart { get; set; }
        //test

        public string ProductDetailId { get; set; }
        [JsonIgnore]
        public virtual ProductDetail? ProductDetail { get; set; }
    }
}

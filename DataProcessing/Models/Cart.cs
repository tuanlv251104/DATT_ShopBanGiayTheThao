using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public decimal? TotalPrice { get; set; }
        public Guid? AccountId {  get; set; }
        [JsonIgnore]
        public virtual ApplicationUser? Account {  get; set; }
        [JsonIgnore]
        public virtual ICollection<CartDetail>? CartDetails { get; set; }
    }
}   

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class OrderHistory
    {
        public Guid Id { get; set; }
        public string StatusType { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Note { get; set; }

        public Guid UpdatedByUserId { get; set; }
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public virtual Order? Order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class Sole
    {
        public Guid Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        public bool Status { get; set; }
    }
}

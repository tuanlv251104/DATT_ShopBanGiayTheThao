using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
    public class Size
    {
        public Guid Id { get; set; }
        [Range(35, 44, ErrorMessage = "Value must be between 35 and 44.")]
        public int Value { get; set; }
        public bool Status { get; set; }
    }
}

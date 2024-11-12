using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Models
{
	public class OrderAdress
	{
        [Key]
		public Guid Id { get; set; }
		[Required]
		public string RecipientName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string AddressDetail { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Commune { get; set; }
        [Required]
        public Guid OrderId { get; set; }

		////12345645
    }
}

using DataProcessing.Models;

namespace API.DTO
{
	public class ProductDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }


		public Guid CategoryId { get; set; }
		public Guid SoleId { get; set; }
		public Guid BrandId { get; set; }
		public Guid MaterialId { get; set; }
	}
}

namespace API.DTO
{
	public class ImageDTO
	{
		public Guid Id { get; set; }
		public string URL { get; set; }
		public Guid ColorId { get; set; }
	}

	public class SelectedImageDTO
	{
		public Guid Id { get; set; }
		public string URL { get; set; }

		public Guid ProductId { get; set; }
		public Guid ColorId { get; set; }
	}
}

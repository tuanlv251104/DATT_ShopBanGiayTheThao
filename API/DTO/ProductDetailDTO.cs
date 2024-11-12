namespace API.DTO
{
    public class ProductDetailDTO
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string MaterialName { get; set; }
        public int sizeValue { get; set; }
        public string ColorName { get; set; }
        public string SoleName { get; set; }
        public decimal PriceProductDetail { get; set; }
    }
}

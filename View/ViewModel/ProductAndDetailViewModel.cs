using DataProcessing.Models;

namespace View.ViewModel
{
    public class ProductAndDetailViewModel
    {
        // Thông tin sản phẩm
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SoleId { get; set; }
        public Guid BrandId { get; set; }
        public Guid MaterialId { get; set; }

        // Các thuộc tính ColorId và SizeId
        public Guid ColorId { get; set; }
        public Guid SizeId { get; set; }

        // Danh sách chi tiết sản phẩm
        public List<ProductDetailViewModel> ProductDetails { get; set; } = new List<ProductDetailViewModel>();

        // Danh sách ảnh của màu
        public IEnumerable<Image>? Images { get; set; }
    }

    public class ProductDetailViewModel
    {
        public string Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public float Weight { get; set; }
        public Guid ColorId { get; set; }
        public Guid SizeId { get; set; }
    }
}

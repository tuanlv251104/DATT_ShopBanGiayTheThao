using API.DTO;
using DataProcessing.Models;

namespace View.IServices
{
    public interface IProductDetailService
    {
        Task<IEnumerable<ProductDetail>> GetAllProductDetail();
        Task<ProductDetail> GetProductDetailById(string id);
        Task Create(ProductDetail productDetail);
        Task Update(ProductDetail productDetail);
        Task Delete(string id);
        Task<List<ProductDetailDTO>> GetVariantsByProductIds(List<Guid> productIds);
        Task<List<ProductDetail>> GetFilteredProductDetails(string? searchQuery = null,
               Guid? colorId = null,
               Guid? sizeId = null,
               Guid? categoryId = null,
               Guid? brandId = null,
               Guid? soleId = null,
               Guid? materialId = null);
    }
}
